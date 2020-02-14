using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Seva.Tests.TestCases
{
    [TestClass]
    public class NormalTests : IDisposable
    {
        private IWebDriver _driver;
        private readonly Uri _createService = new Uri("https://localhost:44322/Market/CreateService");
        private readonly Uri _homeUri = new Uri("https://localhost:44322/");
        private readonly Uri _loginUri = new Uri("https://localhost:44322/Login/Login");
        private readonly Uri _signOutUri = new Uri("https://localhost:44322/Login/SignOut");
        private readonly Uri _registerUri = new Uri("https://localhost:44322/Login/Register");
        private readonly Uri _adminRegisterUri = new Uri("https://localhost:44322/Admin/RegisterAdmin");

        [TestInitialize]
        public void Setup()
        {
            _driver = new ChromeDriver();
        }

        private void Login(string name)
        {
            _driver.Navigate().GoToUrl(_homeUri);
            if (_driver.FindElement(By.Id("footer")).Text.Length > "© 2019 - Seva".Length) _driver.Navigate().GoToUrl(_signOutUri);
            _driver.Navigate().GoToUrl(_loginUri);
            _driver.FindElement(By.Name("Email")).SendKeys($"{name.Substring(0, 1)}.{name}@SEVA.COM");
            _driver.FindElement(By.Name("Password")).SendKeys(name);
            _driver.FindElement(By.Id("loginBtn")).Click();
        }

        private string GenerateRandomString(int charCount)
        {
            var name = "TEST";
            for (int i = 0; i < charCount; i++)
            {
                string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                name += chars[new Random().Next(0, 26)];
            }
            return name;
        }

        [TestMethod]
        public void UC1_TC1_Creating_A_Service()
        {
            //Zie FunctionalityTests/MarketTests/Can_Create_Service()
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void UC1_TC2_Creating_A_Service_Without_Filling_Necessary_Input_Fields_ExpectErrorMessage()
        {
            string userName = "TESTER";
            Login(userName);

            var serviceName = "TESTCREATESERVICE";

            _driver.Navigate().GoToUrl(_createService);
            _driver.FindElement(By.Name("Name")).SendKeys(serviceName);
            _driver.FindElement(By.Name("Category")).Click();
            _driver.FindElement(By.Id("Category1")).Click();
            _driver.FindElement(By.Name("DeliveryTimeDays")).SendKeys("1");
            _driver.FindElement(By.Name("DeliveryTimeHours")).SendKeys("1");
            _driver.FindElement(By.Name("images")).SendKeys(@"C:\Users\jussi\OneDrive\Documenten\GitHub\SevaV4\Seva\Seva\wwwroot\img\TEST.png");

            //Leaving empty:
            _driver.FindElement(By.Name("Price")).SendKeys("");

            _driver.FindElement(By.Name("Description")).SendKeys("TEST");
            _driver.FindElement(By.Id("createServiceBtn")).Click();

            var errorMessageAppears = _driver.PageSource.Contains("Enter a price");

            Assert.IsTrue(errorMessageAppears);
        }

        [TestMethod]
        public void UC2_TC3_Editing_Offered_Service()
        {
            ////Zie FunctionalityTests/ProfileTests/User_Can_Edit_Profile()
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void UC4_TC6_User_Can_Buy_OfferedService()
        {
            //Zie FunctionalityTests/MarketTests/User_Can_Buy_OfferedService()
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void UC5_TC7_User_Can_Write_Review()
        {
            //Zie FunctionalityTests/MarketTests/User_Can_Buy_OfferedService()
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void UC5_TC8_User_Can_Write_Review_Without_Filling_Necessary_Input_Fields_ExpectErrorMessage()
        {
            const string userName2 = "TESTER2";
            Login(userName2);

            _driver.Navigate().GoToUrl(_homeUri);
            _driver.FindElements(By.ClassName("service")).First(element => true).Click();
            _driver.FindElement(By.Id("buyOfferedServiceBtn")).Click();

            //Leaving this empty:
            _driver.FindElement(By.Name("Title")).SendKeys("");

            _driver.FindElement(By.Name("Rating")).Clear();
            _driver.FindElement(By.Name("Rating")).SendKeys("8");
            _driver.FindElement(By.Name("Text")).SendKeys(GenerateRandomString(10));
            _driver.FindElement(By.Id("writeReviewBtn")).Click();

            var errorMessage = "Enter a title";
            var result = _driver.PageSource.Contains(errorMessage);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UC6_TC9_User_Can_Filter_On_Name()
        {
            //Zie FunctionalityTests/HomeTests/User_Can_Filter_On_Name()
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void UC6_TC10_User_Can_Filter_On_Name_ExpectNoResults()
        {
            var nameToSearch = "cbdhkb";

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
        public void UC7_TC11_User_Can_Filter_On_Category()
        {
            //Zie FunctionalityTests/HomeTests/User_Can_Filter_On_Category()
        }

        [TestMethod]
        public void UC7_TC13_User_Can_Filter_On_Category_ExpectAllServicesShown()
        {
            _driver.Navigate().GoToUrl(_homeUri);

            //By default no catefory selected.

            var allServicesAreShown = _driver.FindElements(By.ClassName("service")).All(element => element.Displayed);
            var servicesHidden = _driver.FindElements(By.ClassName("service")).Count(element => !element.Displayed);

            Assert.IsTrue(allServicesAreShown);
            Assert.AreEqual(0, servicesHidden);
        }

        [TestMethod]
        public void UC8_TC14_Admin_Can_See_All_Offered_Services()
        {
            //Zie FunctionalityTests/AdminTests/Admin_Can_See_All_OfferedServices()
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void UC9_TC15_Admin_Can_See_All_Used_Services()
        {
            //Zie FunctionalityTests/AdminTests/Admin_Can_See_All_UsedServices()
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void UC10_TC16_User_Can_View_All_Its_Offered_Services()
        {
            //Zie FunctionalityTests/ProfileTests/User_Can_View_All_Its_Offered_Services()
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void UC11_TC17_User_Can_View_All_Its_Used_Services()
        {
            //Zie FunctionalityTests/ProfileTests/User_Can_View_All_Its_Used_Services()
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void UC14_TC20_User_Can_View_Provider()
        {
            //Zie FunctionalityTests/HomeTests/ViewUser_Can_Be_Loaded()
        }

        [TestMethod]
        public void UC16_TC23_Editing_Profile()
        {
            //Zie FunctionalityTests/ProfileTests/User_Can_Edit_Profile()
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void UC17_TC24_User_Can_Register()
        {
            //Zie FunctionalityTests/LoginTests/User_Can_Register()
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void UC17_TC26_User_Can_Register_Without_Filling_Necessary_Input_Fields_ExpectErrorMessage()
        {
            var firstName = GenerateRandomString(5);
            var lastName = GenerateRandomString(5);
            var password = firstName + lastName;
            _driver.Navigate().GoToUrl(_registerUri);

            _driver.FindElement(By.Name("FirstName")).SendKeys(firstName);
            _driver.FindElement(By.Name("LastName")).SendKeys(lastName);

            //Leaving this empty:
            _driver.FindElement(By.Name("Email")).SendKeys("");

            _driver.FindElement(By.Name("Password")).SendKeys(password);
            _driver.FindElement(By.Name("PasswordConfirm")).SendKeys(password);
            _driver.FindElement(By.Id("registerBtn")).Click();

            var errorMessage = "Enter an email";
            var result = _driver.PageSource.Contains(errorMessage);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UC18_TC27_User_Can_Login()
        {
            //Zie FunctionalityTests/LoginTests/User_Can_Login()
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void UC18_TC28_User_Can_Login_Without_Filling_Necessary_Input_Fields_ExpectErrorMessage()
        {
            var name = "TESTER";
            var password = name;

            _driver.Navigate().GoToUrl(_loginUri);

            _driver.FindElement(By.Name("Email")).SendKeys("");
            _driver.FindElement(By.Name("Password")).SendKeys(password);
            _driver.FindElement(By.Id("loginBtn")).Click();

            var errorMessage = "Please enter your email address";
            var result = _driver.PageSource.Contains(errorMessage);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UC19_TC29_Admin_Can_Register_New_Admin()
        {
            //Zie FunctionalityTests/AdminTests/Admin_Can_Register_New_Admin()
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void UC19_TC30_Admin_Can_Register_New_Admin()
        {
            var adminName = "TESTERADMIN";
            var newAdminName = "TESTERADMIN" + GenerateRandomString(5);
            var email = $"{newAdminName.Substring(0, 1)}.{newAdminName}@seva.com";
            Login(adminName);
            _driver.Navigate().GoToUrl(_adminRegisterUri);
            _driver.FindElement(By.Name("FirstName")).SendKeys(newAdminName);

            //Leaving this empty:
            _driver.FindElement(By.Name("LastName")).SendKeys("");

            _driver.FindElement(By.Name("Email")).SendKeys(email);
            _driver.FindElement(By.Name("Password")).SendKeys(newAdminName);
            _driver.FindElement(By.Name("AdminPassword")).SendKeys(adminName);
            _driver.FindElement(By.Id("adminRegisterBtn")).Click();

            var errorMessage = "Enter last name";
            var result = _driver.PageSource.Contains(errorMessage);

            Assert.IsTrue(result);
        }

        public void Dispose()
        {
            _driver.Close();
            _driver.Dispose();
        }
    }
}
