using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Seva.Tests.FunctionalityTests
{
    [TestClass]
    public class HomeTests : IDisposable
    {
        private IWebDriver _driver;
        private readonly Uri _homeUri = new Uri("https://localhost:44322/");

        [TestInitialize]
        public void Setup()
        {
            _driver = new ChromeDriver();
        }

        [TestMethod]
        public void Homeage_Can_Be_Loaded()
        {
            _driver.Navigate().GoToUrl(_homeUri);
            var result = _driver.Title == "Seva - Welcome";
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void User_Can_Filter_On_Category()
        {
            _driver.Navigate().GoToUrl(_homeUri);

            _driver.FindElement(By.Id("categoryFilter")).Click();
            _driver.FindElement(By.Id("category1")).Click();

            var allServices = _driver.FindElements(By.ClassName("service"));
            var servicesShown = _driver.FindElements(By.ClassName("categoryCategory1"));
            var servicesHidden = allServices.Except(servicesShown);

            bool selectedCategoryItemsAreShown = servicesShown.All(service => service.Displayed);

            bool otherCategoriesAreHidden = servicesHidden.All(service => !service.Displayed);

            Assert.IsTrue(selectedCategoryItemsAreShown);
            Assert.IsTrue(otherCategoriesAreHidden);
        }

        [TestMethod]
        public void User_Can_Filter_On_Name()
        {
            var nameToSearch = "logo";

            _driver.Navigate().GoToUrl(_homeUri);
            
            _driver.FindElement(By.Id("searchBox")).SendKeys(nameToSearch);

            var allServices = _driver.FindElements(By.ClassName("service"));
            var servicesShown = allServices.Where(element => element.Text.Contains(nameToSearch));
            var servicesHidden = allServices.Except(servicesShown);

            bool matchingItemsAreShown = servicesShown.All(service => service.Displayed);

            bool otherCategoriesAreHidden = servicesHidden.All(service => !service.Displayed);

            Assert.IsTrue(matchingItemsAreShown);
            Assert.IsTrue(otherCategoriesAreHidden);
        }

        [TestMethod]
        public void ViewOfferedService_Can_Be_Loaded()
        {
            _driver.Navigate().GoToUrl(_homeUri);
            _driver.FindElements(By.ClassName("service")).First(element => true).Click();

            var result = _driver.Title == "Seva - OfferedService";

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ViewUser_Can_Be_Loaded()
        {
            _driver.Navigate().GoToUrl(_homeUri);
            _driver.FindElements(By.ClassName("service")).First(element => true).Click();

            var result = _driver.Title == "Seva - OfferedService";

            Assert.IsTrue(result);
        }

        public void Dispose()
        {
            _driver.Close();
            _driver.Dispose();
        }
    }
}

