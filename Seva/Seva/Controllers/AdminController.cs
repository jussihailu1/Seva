using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Seva.Factory.Logic;
using Seva.Interface.ILogics;
using Seva.ViewModels;

namespace Seva.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserLogic _userLogic;
        private readonly IOfferedServiceLogic _offeredServiceLogic;
        private readonly IUsedServiceLogic _usedServiceLogic;
        private readonly IImageLogic _imageLogic;
        private readonly IReviewLogic _reviewLogic;

        public AdminController()
        {
            _userLogic = LogicFactory.CreateUserLogic();
            _offeredServiceLogic = LogicFactory.CreateOfferedServiceLogic();
            _usedServiceLogic = LogicFactory.CreateUsedServiceLogic();
            _imageLogic = LogicFactory.CreateImageLogic();
            _reviewLogic = LogicFactory.CreateReviewLogic();
        }

        public IActionResult Index()
        {
            var testMode = _userLogic.GetUserByEmail(User.Identity.Name).FullName.Contains("TEST");

            var users = testMode
                ? _userLogic.GetAllUsers().Where(user => user.Role == 0 && user.FullName.Contains("TEST"))
                : _userLogic.GetAllUsers().Where(user => user.Role == 0 && !user.FullName.Contains("TEST"));

            var model = new AdminIndexViewModel()
            {
                Users = users.Select(user => new AdminUserViewModel(user)).ToList(),
                OfferedServices = new List<AdminOfferedServiceViewModel>(),
                UsedServices = new List<AdminUsedServiceViewModel>()
            };

            foreach (var offeredService in _offeredServiceLogic.GetAllOfferedServices().Where(os => testMode ? os.Name.Contains("TEST") : !os.Name.Contains("TEST")))
            {
                var provider = new UserViewModel(_userLogic.GetUserById(offeredService.ProviderId));
                var amountSold = _usedServiceLogic.GetUsedServicesByOfferdServiceId(offeredService.Id).Count();
                model.OfferedServices.Add(new AdminOfferedServiceViewModel(offeredService, provider, amountSold));
            }

            foreach (var usedService in _usedServiceLogic.GetAllUsedServices().Where(us => testMode ? _userLogic.GetUserById(us.ConsumerId).FullName.Contains("TEST") : !_userLogic.GetUserById(us.ConsumerId).FullName.Contains("TEST")))
            {
                var consumer = new UserViewModel(_userLogic.GetUserById(usedService.ConsumerId));
                var offeredService = new OfferedServiceViewModel(_offeredServiceLogic.GetOfferedServiceById(usedService.OfferedServiceId));
                model.UsedServices.Add(new AdminUsedServiceViewModel(usedService, offeredService, consumer));
            }

            return View(model);
        }

        public IActionResult ViewUser(int userId)
        {
            var user = _userLogic.GetUserById(userId);
            var offeredServices = _offeredServiceLogic.GetOfferedServicesByUserId(user.Id)
                .Select(offeredService => new OfferedServiceViewModel(offeredService)).ToList();
            var usedServices = _usedServiceLogic.GetUsedServicesByConsumerId(userId).Select(usedService =>
                new UsedServiceViewModel(usedService,
                    new OfferedServiceViewModel(_offeredServiceLogic.GetOfferedServiceById(usedService.Id))));
            var offeredServicesCount = offeredServices.Count();
            var soldServicesCount = _offeredServiceLogic.GetOfferedServicesByUserId(user.Id)
                .SelectMany(offeredService => _usedServiceLogic.GetUsedServicesByOfferdServiceId(offeredService.Id))
                .Count();
            var usedServicesCount = _usedServiceLogic.GetUsedServicesByConsumerId(user.Id).Count();

            var model = new AdminUserViewModel(user, offeredServices, usedServices, offeredServicesCount,
                usedServicesCount, soldServicesCount);

            return View(model);
        }

        public IActionResult ViewOfferedService(int offeredServiceId)
        {
            var offeredService = _offeredServiceLogic.GetOfferedServiceById(offeredServiceId);
            var provider = new UserViewModel(_userLogic.GetUserById(offeredService.ProviderId));
            var images = _imageLogic.GetImagesByOfferedServiceId(offeredService.Id).Select(image => image.ToString())
                .ToList();
            var amountSold = _usedServiceLogic.GetUsedServicesByOfferdServiceId(offeredServiceId).Count();

            var usedServices = (from review in _reviewLogic.GetReviewsByOfferedServiceId(offeredServiceId)
                                let usedServiceDateOfPurchase =
                                    _usedServiceLogic.GetUsedServiceById(review.UsedServiceId).DateOfPurchase
                                let reviewViewModel = new ReviewViewModel(review)
                                select new UsedServiceViewModel(usedServiceDateOfPurchase, reviewViewModel)).ToList();

            var model = new AdminOfferedServiceViewModel(offeredService, provider, images, usedServices, amountSold);

            return View(model);
        }

        public IActionResult SetUserStatus(int userId)
        {
            var oldStatus = _userLogic.GetUserById(userId).Status;

            _userLogic.SetStatus(oldStatus, userId);

            return RedirectToAction("ViewUser", "Admin", new { providerId = userId });
        }

        public IActionResult SetOfferedServiceStatus(int offeredServiceId)
        {
            var oldStatus = _offeredServiceLogic.GetOfferedServiceById(offeredServiceId).Status;

            _offeredServiceLogic.SetStatus(offeredServiceId, oldStatus);

            return RedirectToAction("ViewOfferedService", "Admin", new { offeredServiceId = offeredServiceId });
        }

        public IActionResult RegisterAdmin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterAdmin(AdminRegisterViewModel model)
        {

            if (!ModelState.IsValid) return View();

            if (!_userLogic.CheckEmailExistance(model.Email))
            {
                var adminEmail = User.Identity.Name;

                if (_userLogic.CheckAccountExistance(adminEmail, model.AdminPassword))
                {
                    _userLogic.Register(model.FirstName, model.LastName, model.Email, model.Password, 1);

                    TempData["AdminIsCreated"] = true;

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("AdminPassword", "Admin password is not correct");
                return View();
            }

            ModelState.AddModelError("Email", "Email is already in use");
            return View();
        }
    }
}