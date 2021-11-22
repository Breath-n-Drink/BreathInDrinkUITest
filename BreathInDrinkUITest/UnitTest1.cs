using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace BreathInDrinkUITest
{
    [TestClass]
    public class UnitTest1
    {
        private static readonly string DriverDirectory = "C:\\Users\\Mads\\OneDrive\\Dokumenter\\Skole\\webDrivers";
        // Download drivers to your driver folder.
        // Driver version must match your browser version.
        // http://chromedriver.chromium.org/downloads

        private static IWebDriver _driver;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            //_driver = new ChromeDriver(DriverDirectory); // fast
            _driver = new FirefoxDriver(DriverDirectory);  // slow
            //_driver = new EdgeDriver(DriverDirectory); //  not working ...
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [ClassCleanup]
        public static void TearDown()
        {
            _driver.Dispose();
        }

        [TestMethod]
        public void GetAllTest()
        {
            //string url = "file:///C:/andersb/javascript/sayhelloVue3/index.htm";
            string url = "http://127.0.0.1:5500/index.html";
            // string url = "http://localhost:5500/index.htm";
            _driver.Navigate().GoToUrl(url);

            Assert.AreEqual("BreathInDrink", _driver.Title);

            IWebElement inputElement = _driver.FindElement(By.Id("GetMålingButton"));
            inputElement.Click();

            //IWebElement outputElement = _driver.FindElement(By.Id("RecordList"));

            //string text = outputElement.Text;

            //Assert.IsTrue(text.Contains("Danny"));
        }
    }
}
