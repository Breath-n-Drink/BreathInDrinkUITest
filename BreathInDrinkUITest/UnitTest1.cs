using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public void GetMålingTest()
        {
            //string url = "file:///C:/andersb/javascript/sayhelloVue3/index.htm";
            string url = "http://127.0.0.1:5500/index.html";
            // string url = "http://localhost:5500/index.htm";
            _driver.Navigate().GoToUrl(url);

            Assert.AreEqual("BreathNDrink", _driver.Title);

            IWebElement inputElement = _driver.FindElement(By.Id("GetMålingButton"));
            inputElement.Click();

            Thread.Sleep(2000);

            IWebElement outputElement = _driver.FindElement(By.Id("Promille"));
            string text = outputElement.Text;
            Assert.AreEqual("3.4", text);

            
        }

        [TestMethod]
        public void GetListTest()
        {
            //string url = "file:///C:/andersb/javascript/sayhelloVue3/index.htm";
            string url = "http://127.0.0.1:5500/index.html";
            // string url = "http://localhost:5500/index.htm";
            _driver.Navigate().GoToUrl(url);


            //IWebElement outputElement = _driver.FindElement(By.Id("DrinkList"));
            //string text = outputElement.Text;

            //Assert.IsTrue(text.Contains("GG"));
            //Thread.Sleep(3000);

            IList<IWebElement> list = _driver.FindElements(By.Id("DrinkList"));

            list.FirstOrDefault().Click();



            //Switch to active element here in our case its model dialogue box.
            _driver.SwitchTo().ActiveElement();

            Thread.Sleep(3000);

            IWebElement output = _driver.FindElement(By.Id("DrinkName"));
            string text2 = output.Text;

            Assert.AreEqual("GG", text2);

            // find the button which contains text "Yes" as we have dynamic id
            //_driver.FindElement(By.XPath("//button[contains(text(),'Yes')]"));


            // virker ikke
            //IWebElement modal = list.FirstOrDefault();
            //string text2 = modal.Text;
            //Assert.IsTrue(text2.Contains("Measurements"));





        }
    }
}
