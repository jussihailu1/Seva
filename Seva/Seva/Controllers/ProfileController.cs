using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Seva.Factory.Logic;
using Seva.Interface.ILogics;
using Seva.Logic.Logics;
using Seva.Logic.Models;
using Seva.ViewModels;

namespace Seva.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IOfferedServiceLogic _offeredServiceLogic;
        private readonly IUserLogic _userLogic;
        private readonly IImageLogic _imageLogic;
        private readonly IUsedServiceLogic _usedServiceLogic;
        private readonly IReviewLogic _reviewLogic;
        private readonly ICategoryLogic _categoryLogic;

        public ProfileController()
        {
            _offeredServiceLogic = LogicFactory.CreateOfferedServiceLogic();
            _userLogic = LogicFactory.CreateUserLogic();
            _imageLogic = LogicFactory.CreateImageLogic();
            _usedServiceLogic = LogicFactory.CreateUsedServiceLogic();
            _reviewLogic = LogicFactory.CreateReviewLogic();
            _categoryLogic = LogicFactory.CreateCategoryLogic();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var user = _userLogic.GetUserByEmail(User.Identity.Name);

            var model = new ProfileViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Description = user.Description,
                OfferedServiceViewModels = new List<OfferedServiceViewModel>(),
                UsedServiceViewModels = new List<UsedServiceViewModel>()
            };

            foreach (var offeredService in _offeredServiceLogic.GetOfferedServicesByUserId(user.Id))
            {
                var provider = new UserViewModel(_userLogic.GetUserById(offeredService.ProviderId));
                var images = _imageLogic.GetImagesByOfferedServiceId(offeredService.Id)
                    .Select(image => image.ToString()).ToList();
                var offeredServiceViewModel = new OfferedServiceViewModel(offeredService, provider, images);

                model.OfferedServiceViewModels.Add(offeredServiceViewModel);
            }

            foreach (var usedService in _usedServiceLogic.GetUsedServicesByConsumerId(user.Id))
            {
                var reviewViewModel = _reviewLogic.GetReviewsByWriterId(user.Id)
                        .Where(review => review.UsedServiceId == usedService.Id)
                        .Select(review => new ReviewViewModel(review)).FirstOrDefault();

                var offeredService =
                    new OfferedServiceViewModel(
                        _offeredServiceLogic.GetOfferedServiceById(usedService.OfferedServiceId));
                var usedServiceViewModel = new UsedServiceViewModel(usedService, offeredService, reviewViewModel);
                var consumedOfferedService = _offeredServiceLogic.GetOfferedServiceById(usedService.OfferedServiceId);
                var provider = new UserViewModel(_userLogic.GetUserById(consumedOfferedService.ProviderId));
                var images = _imageLogic.GetImagesByOfferedServiceId(consumedOfferedService.Id)
                    .Select(image => image.ToString()).ToList();
                usedServiceViewModel.OfferedService =
                    new OfferedServiceViewModel(consumedOfferedService, provider, images);

                model.UsedServiceViewModels.Add(usedServiceViewModel);
            }

            return View(model);
        }

        public IActionResult EditProfile()
        {
            var user = _userLogic.GetUserByEmail(User.Identity.Name);

            var model = new EditProfileViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                Description = user.Description
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult EditProfile(EditProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var currentUser = _userLogic.GetUserByEmail(User.Identity.Name);

            _userLogic.UpdateUser(currentUser.Id, model.FirstName, model.LastName, model.Email, model.Password,
                model.Description);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult MyOfferedService(int myOfferedServiceId)
        {
            var offeredService = _offeredServiceLogic.GetOfferedServiceById(myOfferedServiceId);
            var images = _imageLogic.GetImagesByOfferedServiceId(myOfferedServiceId).Select(image => image.ToString())
                .ToList();

            var model = new MyOfferedServiceViewModel()
            {
                OfferedService = new OfferedServiceViewModel(offeredService, images),
                AmountUsed = _usedServiceLogic.GetUsedServicesByOfferdServiceId(myOfferedServiceId).Count(),
                Reviews = _reviewLogic.GetReviewsByOfferedServiceId(myOfferedServiceId)
                    .Select(review => new ReviewViewModel(review)).ToList()
            };

            return View(model);
        }

        public IActionResult EditOfferedService(int offeredServiceId)
        {
            var offeredService = _offeredServiceLogic.GetOfferedServiceById(offeredServiceId);
            ViewBag.Categories = _categoryLogic.GetAllCategories().Select(category => category.Name);
            var model = new EditOfferedServiceViewModel()
            {
                Id = offeredServiceId,
                Name = offeredService.Name,
                Category = offeredService.Category,
                Price = offeredService.Price,
                Description = offeredService.Description,
                DeliveryTimeDays = offeredService.DeliveryTimeDays,
                DeliveryTimeHours = offeredService.DeliveryTimeHours,
                Images = _imageLogic.GetImagesByOfferedServiceId(offeredServiceId).Select(image => image.ToString())
                    .ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult EditOfferedService(EditOfferedServiceViewModel model)
        {
            _offeredServiceLogic.EditOfferedService(model.Id, model.Name, model.Category, model.DeliveryTimeDays,
                model.DeliveryTimeHours, model.Description, model.Price);

            TempData["ServiceIsEdited"] = true;

            return RedirectToAction("Index", "Profile");
        }

        public IActionResult SetOfferedServiceStatus(int offeredServiceId)
        {
            int oldStatus = _offeredServiceLogic.GetOfferedServiceById(offeredServiceId).Status;

            _offeredServiceLogic.SetStatus(offeredServiceId, oldStatus);

            string newStatus = oldStatus == 0 ? "Inactive" : "Active";
            ViewData["IsSet" + newStatus] = true;

            return RedirectToAction("MyOfferedService", "Profile", new {myOfferedServiceId = offeredServiceId});
        }
    }
}
