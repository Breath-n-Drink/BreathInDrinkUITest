using BreathInDrinkUITest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace BreathInDrinkUITest
{
    [TestClass]
    public class UnitTest1
    {
        private static readonly string DriverDirectory = "C:\\Users\\mads6\\OneDrive\\Dokumenter\\Kode\\webDrivers";
        private static BreathndrinkContext _context = new BreathndrinkContext(); 
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
            string url = "https://breathndrinkvue.azurewebsites.net/";
            // string url = "http://localhost:5500/index.htm";
            _driver.Navigate().GoToUrl(url);
            double GetPromillle()
            {
                Promille result = _context.Promille.ToList()[^1];
                return result.Promille1;
            }

            Assert.AreEqual("BreathNDrink", _driver.Title);

            IWebElement inputElement = _driver.FindElement(By.Id("GetMålingButton"));
            inputElement.Click();

            Thread.Sleep(2000);

            IWebElement outputElement = _driver.FindElement(By.Id("Promille"));
            string textActual = outputElement.Text;
            string textExpected = Math.Round(GetPromillle(), 1).ToString();
            Assert.AreEqual(Math.Round(GetPromillle(), 1).ToString(CultureInfo.InvariantCulture), textActual);
        }

        [TestMethod]
        public void GetListTest()
        {
            //string url = "file:///C:/andersb/javascript/sayhelloVue3/index.htm";
            string url = "https://breathndrinkvue.azurewebsites.net/";
            // string url = "http://localhost:5500/index.htm";
            _driver.Navigate().GoToUrl(url);


            IWebElement outputElement = _driver.FindElement(By.Id("DrinkList"));
            string text = outputElement.Text;
            Assert.IsTrue(text.Contains("GG"));
        }

        [TestMethod]
        public void GetModalTest()
        {
            //string url = "file:///C:/andersb/javascript/sayhelloVue3/index.htm";
            string url = "https://breathndrinkvue.azurewebsites.net/";
            // string url = "http://localhost:5500/index.htm";
            _driver.Navigate().GoToUrl(url);
            

            IList<IWebElement> list = _driver.FindElements(By.Id("DrinkList"));
            list.FirstOrDefault().Click();

            //Switch to active element here in our case its model dialogue box.
            _driver.SwitchTo().ActiveElement();
            IWebElement nameOutput = _driver.FindElement(By.Id("DrinkName"));
            IWebElement ingredientsOutput = _driver.FindElement(By.Id("Ingredients"));
            IWebElement alcoholOutput = _driver.FindElement(By.Id("AlcoholPercentage"));
            IWebElement measurementsOutput = _driver.FindElement(By.Id("Measurements"));
            string modalDrinkName = nameOutput.Text;
            string modalIngredients = ingredientsOutput.Text;
            string modalAlcohol = alcoholOutput.Text;
            string modalMeasurements = measurementsOutput.Text;
            Assert.AreEqual("A1", modalDrinkName);
            Assert.IsTrue(modalIngredients.Contains("Gin"));
            Assert.AreEqual("Alkohol 39%", modalAlcohol);
            Assert.IsTrue(modalMeasurements.Contains("1 3/4"));

            

            // find the button which contains text "Yes" as we have dynamic id
            //_driver.FindElement(By.XPath("//button[contains(text(),'Yes')]"));
        }
    }
}
