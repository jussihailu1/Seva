using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Seva.Factory.Logic;
using Seva.Interface.ILogics;
using Seva.ViewModels;

namespace Seva.Controllers
{
    public class MarketController : Controller
    {
        private readonly IOfferedServiceLogic _offeredServiceLogic;
        private readonly IUserLogic _userLogic;
        private readonly ICategoryLogic _categoryLogic;
        private readonly IImageLogic _imageLogic;
        private readonly IReviewLogic _reviewLogic;

        public MarketController()
        {
            _offeredServiceLogic = LogicFactory.CreateOfferedServiceLogic();
            _userLogic = LogicFactory.CreateUserLogic();
            _categoryLogic = LogicFactory.CreateCategoryLogic();
            _imageLogic = LogicFactory.CreateImageLogic();
            _reviewLogic = LogicFactory.CreateReviewLogic();
        }

        public IActionResult CreateService()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login");
            }

            ViewBag.Categories = _categoryLogic.GetAllCategories().Select(category => category.Name);
            return View();
        }

        [HttpPost]
        public IActionResult CreateService(CreateServiceViewModel model, List<IFormFile> images)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login");
            }

            if (!ModelState.IsValid)
            {
                if (model.Category == "Select a category")
                {
                    ModelState.AddModelError("Category", "Select a category");
                }

                return View();
            }

            var currentUserId = _userLogic.GetUserByEmail(User.Identity.Name).Id;

            _offeredServiceLogic.CreateService(model.Name, model.Category, model.DeliveryTimeDays,
                model.DeliveryTimeHours, model.Description, model.Price, currentUserId);

            if (images != null && images.Count > 0)
            {
                _imageLogic.UploadImages(images, currentUserId);
            }

            TempData["ServiceIsCreated"] = true;

            var myCreatedServiceId = _offeredServiceLogic.GetLatestOfferedServiceIdByProviderId(currentUserId);

            return RedirectToAction("MyOfferedService", "Profile", new { myOfferedServiceId = myCreatedServiceId });
        }

        public IActionResult PurchaseService(int offeredServiceId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login");
            }

            var consumerId = _userLogic.GetUserByEmail(User.Identity.Name).Id;
            _offeredServiceLogic.PurchaseOfferedService(offeredServiceId, consumerId);

            TempData["ServiceIsPurchased"] = true;
            TempData["offeredServiceId"] = offeredServiceId;

            return RedirectToAction("WriteReview", "Market", new { offeredServiceId = offeredServiceId });
        }

        public IActionResult WriteReview(int offeredServiceId)
        {
            var model = GetOfferedServiceViewModel(offeredServiceId);
            TempData["offeredServiceId"] = model.OfferedService.Id;
            return View(model);
        }

        private WriteReviewViewModel GetOfferedServiceViewModel(int offeredServiceId)
        {
            var offeredService = _offeredServiceLogic.GetOfferedServiceById(offeredServiceId);
            var images = _imageLogic.GetImagesByOfferedServiceId(offeredServiceId).Select(image => image.ImageAsString)
                .ToList();
            var provider = new UserViewModel(_userLogic.GetUserById(offeredService.ProviderId));

            var model = new WriteReviewViewModel()
            {
                OfferedService = new OfferedServiceViewModel(offeredService, provider, images)
            };

            return model;
        }

        [HttpPost]
        public IActionResult WriteReview(WriteReviewViewModel model)
        {
            if (!ModelState.IsValid)
            {
                if (model.Rating == 0)
                {
                    ModelState.AddModelError("Rating", "This can't be 0");
                }

                var offeredServiceId = Convert.ToInt32(TempData["offeredServiceId"]);
                model = GetOfferedServiceViewModel(offeredServiceId);

                return View(model);
            }

            var currentUser = _userLogic.GetUserByEmail(User.Identity.Name);

            _reviewLogic.WriteReview(currentUser.Id, model.Title, model.Text, model.Rating);

            TempData["ReviewIsPosted"] = true;
            TempData["offeredServiceViewModel"] = null;
            return RedirectToAction("ViewOfferedService", "Home", new { offeredServiceId = TempData["offeredServiceId"] });
        }
    }
}
