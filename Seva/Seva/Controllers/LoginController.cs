using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Seva.Factory.Logic;
using Seva.Interface.ILogics;
using Seva.ViewModels;

namespace Seva.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserLogic _userLogic;

        public LoginController()
        {
            _userLogic = LogicFactory.CreateUserLogic();
        }

        public IActionResult Login()
        {
            return View();
        }

        [Route("CheckEmailExistance/{emailToCheck}")]
        public IActionResult CheckEmailExistance(string emailToCheck) => _userLogic.CheckEmailExistance(emailToCheck) ? new JsonResult("Exists") : new JsonResult("Does not exist");

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (_userLogic.CheckAccountExistance(model.Email, model.Password))
            {
                await Login(model.Email);

                var user = _userLogic.GetUserByEmail(model.Email);

                return RedirectToAction("Index", user.Role == 1 ? "Admin" : "Home");
            }

            ModelState.AddModelError("", "Login failed");
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (_userLogic.CheckEmailExistance(model.Email))
            {
                ModelState.AddModelError("Email", "Email is already in use");
                return View();
            }

            _userLogic.Register(model.FirstName, model.LastName, model.Email, model.Password, 0);
            await Login(model.Email);
            TempData["AccountIsCreated"] = true;
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Login");
        }

        private async Task Login(string email)
        {
            var user = _userLogic.GetUserByEmail(email);

            ViewData["UserFullName"] = user.FullName;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, Convert.ToInt16(user.Role).ToString())
            };

            var userIdentity = new ClaimsIdentity(claims, "login");

            var principal = new ClaimsPrincipal(userIdentity);
            await HttpContext.SignInAsync(principal);
        }
    }
}