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
        //private static readonly string DriverDirectory = "C:\\Users\\Mads\\OneDrive\\Dokumenter\\Skole\\webDrivers";
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
            string url = "https://breathndrinkvue.azurewebsites.net/";
            //string url = "http://127.0.0.1:5500/index.html";
            _driver.Navigate().GoToUrl(url);
            Thread.Sleep(1000);

            _driver.SwitchTo().ActiveElement();
            IWebElement noButton = _driver.FindElement(By.Id("noButton"));
            noButton.Click();

            Thread.Sleep(1000);

            IWebElement ageText = _driver.FindElement(By.Id("ageAlert"));
            string age = ageText.Text;
            Assert.IsTrue(age.Contains("18"));

            IWebElement yesButton = _driver.FindElement(By.Id("yesButton"));
            yesButton.Click();

            Thread.Sleep(2000);

            double GetPromillle()
            {
                Promille result = _context.Promille.ToList()[^1];
                return result.Promille1;
            }

            Assert.AreEqual("BreathNDrink", _driver.Title);

            //Checker måling knap og udskreven promille
            IWebElement inputElement = _driver.FindElement(By.Id("GetMålingButton"));
            inputElement.Click();
            Thread.Sleep(3000);
            IWebElement outputElement = _driver.FindElement(By.Id("Promille"));
            string textActual = outputElement.Text;
            string textExpected = Math.Round(GetPromillle(), 1).ToString();
            Assert.AreEqual(Math.Round(GetPromillle(), 1).ToString(CultureInfo.InvariantCulture), textActual);

            //checker person input
            IWebElement weightElement = _driver.FindElement(By.Id("weightField"));
            weightElement.Clear();
            weightElement.SendKeys("70");

            IWebElement currentBacFieldElement = _driver.FindElement(By.Id("currentBacField"));
            currentBacFieldElement.Clear();
            currentBacFieldElement.SendKeys("2.5");

            IWebElement maxBacFieldElement = _driver.FindElement(By.Id("maxBacField"));
            maxBacFieldElement.Clear();
            maxBacFieldElement.SendKeys("3");

            IWebElement genderElement = _driver.FindElement(By.Id("genderToggleMan"));
            genderElement.Click();

            IWebElement recommendElement = _driver.FindElement(By.Id("recommendButton"));
            recommendElement.Click();

            IWebElement result = _driver.FindElement(By.Id("DrinkList"));
            string text = result.Text;
            Assert.IsFalse(text.Contains("GG"));
        }

        [TestMethod]
        public void GetListTest()
        {
            //string url = "file:///C:/andersb/javascript/sayhelloVue3/index.htm";
            string url = "https://breathndrinkvue.azurewebsites.net/";
            // string url = "http://localhost:5500/index.htm";
            _driver.Navigate().GoToUrl(url);
            Thread.Sleep(1000);
            _driver.SwitchTo().ActiveElement();
            IWebElement Button = _driver.FindElement(By.Id("yesButton"));
            Button.Click();

            Thread.Sleep(1000);

            IWebElement showAllElement = _driver.FindElement(By.Id("showAllButton"));
            showAllElement.Click();

            IWebElement outputElement = _driver.FindElement(By.Id("DrinkList"));
            string text = outputElement.Text;
            Assert.IsTrue(text.Contains("GG"));
            Assert.IsTrue(text.Contains("PromilleChange"));
        }

        [TestMethod]
        public void GetModalTest()
        {
            //string url = "file:///C:/andersb/javascript/sayhelloVue3/index.htm";
            string url = "https://breathndrinkvue.azurewebsites.net/";
            // string url = "http://localhost:5500/index.htm";
            _driver.Navigate().GoToUrl(url);
            Thread.Sleep(1000);
            _driver.SwitchTo().ActiveElement();
            IWebElement yesButton = _driver.FindElement(By.Id("yesButton"));
            yesButton.Click();
            Thread.Sleep(1000);

            IWebElement showAllElement = _driver.FindElement(By.Id("showAllButton"));
            showAllElement.Click();

            Thread.Sleep(2000);

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

        [TestMethod]
        public void FilterTest()
        {
            //string url = "file:///C:/andersb/javascript/sayhelloVue3/index.htm";
            string url = "https://breathndrinkvue.azurewebsites.net/";
            // string url = "http://localhost:5500/index.htm";
            _driver.Navigate().GoToUrl(url);
            Thread.Sleep(1000);
            _driver.SwitchTo().ActiveElement();
            IWebElement yesButton = _driver.FindElement(By.Id("yesButton"));
            yesButton.Click();
            Thread.Sleep(1000);

            IWebElement showAllElement = _driver.FindElement(By.Id("showAllButton"));
            showAllElement.Click();

            IWebElement filterButton = _driver.FindElement(By.Id("filterButton"));
            filterButton.Click();

            IWebElement vodkaElement = _driver.FindElement(By.Id("filterItem"));
            vodkaElement.Click();

            Thread.Sleep(2000);

            //IWebElement rumElement = _driver.FindElement(By.Id("rumFilter"));
            //rumElement.Click();

            IWebElement outputElement = _driver.FindElement(By.Id("DrinkList"));
            string text = outputElement.Text;
            Assert.IsFalse(text.Contains("GG"));

            IWebElement outputElement2 = _driver.FindElement(By.Id("DrinkList"));
            string text2 = outputElement2.Text;
            Assert.IsTrue(text2.Contains("Aztec Punch"));

            IWebElement alkoholFilter = _driver.FindElement(By.Id("customRange3"));
            Assert.Fail();
        }
    }
}
