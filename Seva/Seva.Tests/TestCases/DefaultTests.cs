using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Seva.Tests.TestCases
{
    [TestClass]
    public class DefaultTests : IDisposable
    {
        private IWebDriver _driver;
        private readonly Uri _login = new Uri("https://localhost:44322/Login/Login");
        private readonly Uri _createService = new Uri("https://localhost:44322/Market/CreateService");

        [TestInitialize]
        public void Setup()
        {
            _driver = new ChromeDriver();
        }

        [TestMethod]
        public void TC1_Input_5_ExpectedResult_Nothing()
        {
            Login();
            _driver.Navigate().GoToUrl(_createService);
            var textBox = _driver.FindElement(By.Name("DeliveryTimeDays"));
            textBox.SendKeys("vijf");
            var result = textBox.Text == string.Empty;

            Assert.IsTrue(result);
        }

        private void Login()
        {
            _driver.Navigate().GoToUrl(_login);
            _driver.FindElement(By.Name("Email")).SendKeys("T.TESTER@SEVA.COM");
            _driver.FindElement(By.Name("Password")).SendKeys("TESTER");
            _driver.FindElement(By.Id("loginBtn")).Click();
        }

        [TestMethod]
        public void TC2_Input_5_ExpectedResult_Nothing()
        {
            Login();
            _driver.Navigate().GoToUrl(_createService);
            var textBox = _driver.FindElement(By.Name("Price"));
            textBox.SendKeys("twintig");
            var result = textBox.Text == string.Empty;

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TC4_Input_johnDoe_ExpectedResult_ErrorMessage()
        {
            var errorMessage = "Please enter a valid email address.";
            _driver.Navigate().GoToUrl(_login);
            _driver.FindElement(By.Name("Email")).SendKeys("T.TESTER");
            _driver.FindElement(By.Name("Password")).Click();
            var errorMessageAppears = _driver.PageSource.Contains(errorMessage);
            Assert.IsTrue(errorMessageAppears);
        }

        public void Dispose()
        {
            _driver.Close();
            _driver.Dispose();
        }
    }
}
