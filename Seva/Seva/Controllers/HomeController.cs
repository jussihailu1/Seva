using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Seva.Factory.Logic;
using Seva.Interface.ILogicModels;
using Seva.Interface.ILogics;
using Seva.Models;
using Seva.ViewModels;

namespace Seva.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IOfferedServiceLogic _offeredServiceLogic;
        private readonly IUserLogic _userLogic;
        private readonly IImageLogic _imageLogic;
        private readonly ICategoryLogic _categoryLogic;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _offeredServiceLogic = LogicFactory.CreateOfferedServiceLogic();
            _userLogic = LogicFactory.CreateUserLogic();
            _imageLogic = LogicFactory.CreateImageLogic();
            _categoryLogic = LogicFactory.CreateCategoryLogic();
        }

        public IActionResult Index()
        {
            if (User.IsInRole("1")) return RedirectToAction("Index", "Admin");

            ViewData["Categories"] = _categoryLogic.GetAllCategories().Select(category => category.Name).ToList();

            var model = new HomeViewModel() { OfferedServices = new List<OfferedServiceViewModel>() };

            IEnumerable<IOfferedService> offeredServices;

            if (!User.Identity.IsAuthenticated)
            {
                offeredServices = _offeredServiceLogic.GetAllOfferedServices().Where(offeredService =>
                    offeredService.Status == 0 && !offeredService.Name.Contains("TEST"));
            }
            else
            {
                var currentUser = _userLogic.GetUserByEmail(User.Identity.Name);
                if (!currentUser.FullName.Contains("TEST"))
                {
                    offeredServices = _offeredServiceLogic.GetAllOfferedServices().Where(offeredService =>
                        offeredService.Status == 0 && offeredService.ProviderId != currentUser.Id &&
                        !offeredService.Name.Contains("TEST"));
                }
                else
                {
                    offeredServices = _offeredServiceLogic.GetAllOfferedServices().Where(offeredService =>
                        offeredService.Status == 0 && offeredService.ProviderId != currentUser.Id &&
                        offeredService.Name.Contains("TEST"));
                }
            }

            foreach (var offeredService in offeredServices.OrderByDescending(offeredService => offeredService.DateOfPost))
            {
                var provider = new UserViewModel(_userLogic.GetUserById(offeredService.ProviderId));
                var images = _imageLogic.GetImagesByOfferedServiceId(offeredService.Id)
                    .Select(image => image.ToString()).ToList();
                model.OfferedServices.Add(new OfferedServiceViewModel(offeredService, provider, images));
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ViewProvider(int providerId)
        {
            var user = _userLogic.GetUserById(providerId);

            var offeredServices = _offeredServiceLogic.GetOfferedServicesByUserId(providerId)
                .Select(offeredService => new OfferedServiceViewModel(offeredService, _imageLogic
                    .GetImagesByOfferedServiceId(offeredService.Id)
                    .Select(image => image.ImageAsString).ToList())).ToList();

            var model = new UserViewModel(user, offeredServices);

            return View(model);
        }

        [HttpGet]
        public IActionResult ViewOfferedService(int offeredServiceId)
        {
            var offeredService = _offeredServiceLogic.GetOfferedServiceById(offeredServiceId);
            var provider = new UserViewModel(_userLogic.GetUserById(offeredService.ProviderId));
            var images = _imageLogic.GetImagesByOfferedServiceId(offeredService.Id).Select(image => image.ToString())
                .ToList();

            var model = new OfferedServiceViewModel(offeredService, provider, images);

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

