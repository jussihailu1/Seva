using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Seva.Tests.FunctionalityTests
{
    [TestClass]
    public class ProfileTests : IDisposable
    {
        private IWebDriver _driver;
        private readonly Uri _homeUri = new Uri("https://localhost:44322/");
        private readonly Uri _loginUri = new Uri("https://localhost:44322/Login/Login");
        private readonly Uri _profileUri = new Uri("https://localhost:44322/Profile/Index");
        private readonly Uri _editProfileUri = new Uri("https://localhost:44322/Profile/EditProfile");
        private readonly Uri _signOutUri = new Uri("https://localhost:44322/Login/SignOut");
        private const string userName1 = "TESTER";
        private const string userName2 = "TESTER2";

        [TestInitialize]
        public void Setup()
        {
            _driver = new ChromeDriver(); 
            Login(userName1);
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
        public void Profile_Can_Be_Loaded()
        {
            _driver.Navigate().GoToUrl(_profileUri);
            var result = _driver.Title == "Seva - Profile";
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void User_Can_View_All_Its_Offered_Services()
        {
            _driver.Navigate().GoToUrl(_profileUri);
            _driver.FindElement(By.Id("offeredServicesTab")).Click();
            //This will only work if the user has offered atleast 1 service.
            var result = _driver.FindElements(By.ClassName("offeredService")).Count > 0 && _driver.FindElements(By.ClassName("offeredService")).All(element => element.Displayed);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void User_Can_View_All_Its_Used_Services()
        {
            Login(userName2);
            _driver.Navigate().GoToUrl(_profileUri);
            _driver.FindElement(By.Id("usedServicesTab")).Click();
            //This will only work if the user has offered atleast 1 service.
            var result = _driver.FindElements(By.ClassName("usedService")).Count > 0 && _driver.FindElements(By.ClassName("usedService")).All(element => element.Displayed);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void EditProfile_Can_Be_Loaded()
        {
            _driver.Navigate().GoToUrl(_editProfileUri);
            var result = _driver.Title == "Seva - Edit profile";
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void User_Can_Edit_Profile()
        {
            _driver.Navigate().GoToUrl(_editProfileUri);
            //var currentDescription = _driver.FindElement(By.Name("Description")).Text;
            var newDescription = GenerateRandomDescription(10);
            _driver.FindElement(By.Name("Description")).Clear();
            _driver.FindElement(By.Name("Description")).SendKeys(newDescription);
            _driver.FindElement(By.Id("editProfileBtn")).Click();
            _driver.Navigate().GoToUrl(_editProfileUri);

            var newCurrentDescription = _driver.FindElement(By.Name("Description")).Text;

            Assert.AreEqual(newDescription, newCurrentDescription);
        }

        [TestMethod]
        public void EditOfferedService_Can_Be_Loaded()
        {
            _driver.Navigate().GoToUrl(_profileUri);
            _driver.FindElement(By.Id("offeredServicesTab")).Click();
            const string serviceName = "TESTCREATESERVICE";

            var offeredServicesByUser = _driver.PageSource.Split(serviceName).Length;
            if (offeredServicesByUser < 1)
            {
                Assert.Fail("The user has no offered services to edit.");
            }

            _driver.FindElements(By.ClassName("offeredService"))[0].Click();
            _driver.FindElement(By.Id("editOfferedServiceLink")).Click();

            var result = _driver.Title == "Seva - Edit offered service";
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void User_Can_EditOfferedService()
        {
            _driver.Navigate().GoToUrl(_profileUri);
            _driver.FindElement(By.Id("offeredServicesTab")).Click();

            const string serviceName = "TESTCREATESERVICE";

            var offeredServicesByUser = _driver.PageSource.Split(serviceName).Length;
            if (offeredServicesByUser < 1)
            {
                Assert.Fail("The user has no offered services to edit.");
            }

            _driver.FindElements(By.ClassName("offeredService"))[0].Click();
            _driver.FindElement(By.Id("editOfferedServiceLink")).Click();
            var newDescription = GenerateRandomDescription(10);
            _driver.FindElement(By.Name("Description")).Clear();
            _driver.FindElement(By.Name("Description")).SendKeys(newDescription);
            _driver.FindElement(By.Id("editOfferedServiceBtn")).Click();

            _driver.Navigate().GoToUrl(_profileUri);
            _driver.FindElement(By.Id("offeredServicesTab")).Click();
            _driver.FindElements(By.ClassName("offeredService"))[0].Click();
            _driver.FindElement(By.Id("editOfferedServiceLink")).Click();
            var result = _driver.PageSource.Contains(newDescription);
            Assert.IsTrue(result);
        }

        private string GenerateRandomDescription(int charCount)
        {
            var description = "TEST";
            for (int i = 0; i < charCount; i++)
            {
                string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                description += chars[new Random().Next(0, 36)];
            }
            return description;
        }

        public void Dispose()
        {
            _driver.Close();
            _driver.Dispose();
        }
    }
}
