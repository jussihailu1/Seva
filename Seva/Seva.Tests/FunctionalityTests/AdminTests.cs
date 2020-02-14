using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Seva.Tests.FunctionalityTests
{
    [TestClass]
    public class AdminTests : IDisposable
    {
        private readonly Uri _homeUri = new Uri("https://localhost:44322/");
        private readonly Uri _loginUri = new Uri("https://localhost:44322/Login/Login");
        private readonly Uri _adminHomeUri = new Uri("https://localhost:44322/Admin/Index");
        private readonly Uri _signOutUri = new Uri("https://localhost:44322/Login/SignOut");
        private readonly Uri _adminRegisterUri = new Uri("https://localhost:44322/Admin/RegisterAdmin");
        private IWebDriver _driver;
        private const string AdminName = "TESTERADMIN";

        [TestInitialize]
        public void Setup()
        {
            _driver = new ChromeDriver();
        }

        private void Login(string userName)
        {
            _driver.Navigate().GoToUrl(_homeUri);
            if (_driver.FindElement(By.Id("footer")).Text.Length > "© 2019 - Seva".Length) _driver.Navigate().GoToUrl(_signOutUri);
            _driver.Navigate().GoToUrl(_loginUri);
            _driver.FindElement(By.Name("Email")).SendKeys($"{userName.Substring(0, 1)}.{userName}@SEVA.COM");
            _driver.FindElement(By.Name("Password")).SendKeys(userName);
            _driver.FindElement(By.Id("loginBtn")).Click();
        }

        [TestMethod]
        public void Can_Be_Loaded()
        {
            Login(AdminName);
            _driver.Navigate().GoToUrl(_adminHomeUri);
            var result = _driver.Title == "Seva - Admin";

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Admin_Can_See_All_OfferedServices()
        {
            Login(AdminName);
            _driver.Navigate().GoToUrl(_adminHomeUri);
            _driver.FindElement(By.Id("adminOfferedServicesTab")).Click();
            //This will only work if there actually are offered services by test accounts.
            var result = _driver.FindElements(By.ClassName("service")).Count > 0 && _driver.FindElements(By.ClassName("service")).All(element => element.Displayed);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Admin_Can_See_All_UsedServices()
        {
            Login(AdminName);
            _driver.Navigate().GoToUrl(_adminHomeUri);
            _driver.FindElement(By.Id("adminUsedServicesTab")).Click();
            //This will only work if there actually are used services by test accounts.
            var result = _driver.FindElements(By.ClassName("usedService")).Count > 0 && _driver.FindElements(By.ClassName("usedService")).All(element => element.Displayed);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Admin_Can_Register_New_Admin()
        {
            var newAdminName = "TESTERADMIN" + GenerateRandomString(5);
            var email = $"{newAdminName.Substring(0, 1)}.{newAdminName}@seva.com";
            Login(AdminName);
            _driver.Navigate().GoToUrl(_adminRegisterUri);
            _driver.FindElement(By.Name("FirstName")).SendKeys(newAdminName);
            _driver.FindElement(By.Name("LastName")).SendKeys(newAdminName);
            _driver.FindElement(By.Name("Email")).SendKeys(email);
            _driver.FindElement(By.Name("Password")).SendKeys(newAdminName);
            _driver.FindElement(By.Name("AdminPassword")).SendKeys(AdminName);
            _driver.FindElement(By.Id("adminRegisterBtn")).Click();


            Login(newAdminName);
            _driver.Navigate().GoToUrl(_adminHomeUri);
            var footerText = _driver.FindElement(By.Id("footer")).Text;

            var userIsOnAdminHomePage = _driver.Title == "Seva - Admin";
            var userNameIsDisplayed = footerText.Contains(newAdminName);

            Assert.IsTrue(userIsOnAdminHomePage);
            Assert.IsTrue(userNameIsDisplayed);
        }


        private string GenerateRandomString(int charCount)
        {
            var name = "";
            for (int i = 0; i < charCount; i++)
            {
                string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                name += chars[new Random().Next(0, 26)];
            }
            return name;
        }

        public void Dispose()
        {
            _driver.Close();
            _driver.Dispose();
        }
    }
}
