using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Seva.Tests.FunctionalityTests
{
    [TestClass]
    public class LoginTests : IDisposable
    {
        private IWebDriver _driver;
        private readonly Uri _loginUri = new Uri("https://localhost:44322/Login/Login");
        private readonly Uri _registerUri = new Uri("https://localhost:44322/Login/Register");

        [TestInitialize]
        public void Setup()
        {
            _driver = new ChromeDriver();
        }

        [TestMethod]
        public void Login_Can_Be_Loaded()
        {
            _driver.Navigate().GoToUrl(_loginUri);
            var result = _driver.Title == "Seva - Login";

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void User_Can_Login()
        {
            var name = "TESTER";
            var email = $"{name.Substring(0,1)}.{name}@seva.com";
            var password = name;
            var fullName = $"{name} {name}";

            _driver.Navigate().GoToUrl(_loginUri);

            _driver.FindElement(By.Name("Email")).SendKeys(email);
            _driver.FindElement(By.Name("Password")).SendKeys(password); 
            _driver.FindElement(By.Id("loginBtn")).Click();

            var footerText = _driver.FindElement(By.Id("footer")).Text;

            var userIsOnHomePage = _driver.Title == "Seva - Welcome";
            var userNameIsDisplayed = footerText.Contains(fullName);

            Assert.IsTrue(userIsOnHomePage);
            Assert.IsTrue(userNameIsDisplayed);
        }

        [TestMethod]
        public void Register_Can_Be_Loaded()
        {
            _driver.Navigate().GoToUrl(_registerUri);
            var result = _driver.Title == "Seva - Register";

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void User_Can_Register()
        {
            //Wanneer deze test gedebugt wordt werkt het wel.

            var firstName = GenerateRandomString(5);
            var lastName = GenerateRandomString(5);
            var email = $"{firstName.Substring(0, 1)}.{lastName}@seva.com";
            var password = firstName + lastName;
            var fullName = $"{firstName} {lastName}";
            _driver.Navigate().GoToUrl(_registerUri);

            _driver.FindElement(By.Name("FirstName")).SendKeys(firstName);
            _driver.FindElement(By.Name("LastName")).SendKeys(lastName);
            _driver.FindElement(By.Name("Email")).SendKeys(email);
            _driver.FindElement(By.Name("Password")).SendKeys(password);
            _driver.FindElement(By.Name("PasswordConfirm")).SendKeys(password);
            _driver.FindElement(By.Id("registerBtn")).Click();

            var footerText = _driver.FindElement(By.Id("footer")).Text;

            var userIsOnHomePage = _driver.Title == "Seva - Welcome";
            var userNameIsDisplayed = footerText.Contains(fullName);

            Assert.IsTrue(userIsOnHomePage);
            Assert.IsTrue(userNameIsDisplayed);
        }

        private string GenerateRandomString(int charCount)
        {
            var name = "TEST";
            for (int i = 0; i < charCount; i++)
            {
                string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                name += chars[new Random().Next(0, 27)];
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
