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
        //private static readonly string DriverDirectory = "C:\\Users\\mads6\\OneDrive\\Dokumenter\\Kode\\webDrivers";
        private static readonly string DriverDirectory = "C:\\Users\\Mads\\OneDrive\\Dokumenter\\Skole\\webDrivers";
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

            string url = "https://breathndrinkvue.azurewebsites.net/";

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

            IWebElement nameInput = _driver.FindElement(By.Id("inputName"));
            nameInput.Clear();
            nameInput.SendKeys("Mads");

            IWebElement submit = _driver.FindElement(By.Id("SubmitButton"));
            submit.Click();

            Thread.Sleep(1000);

            IWebElement inputElement = _driver.FindElement(By.Id("GetAlchoholLevelButton"));
            inputElement.Click();

            _driver.SwitchTo().ActiveElement();

            Thread.Sleep(3000);

            IWebElement doneElement = _driver.FindElement(By.Id("doneButton"));
            doneElement.Click();

            Thread.Sleep(3000);
        }

        [ClassCleanup]
        public static void TearDown()
        {
            _driver.Dispose();
        }

        [TestMethod]
        public void GetMeasurementTest()
        {
          double GetPromillle()
            {
                Promille result = _context.Promille.ToList()[^1];
                return result.Promille1;
            }

            Assert.AreEqual("BreathNDrink", _driver.Title);

            //Checker måling knap og udskreven promille
            

            double promille = GetPromillle();
            if (promille < 0.7)
            {
                IWebElement outputElement = _driver.FindElement(By.Id("Promille1"));
                string textActual = outputElement.Text;
                Assert.AreEqual(Math.Round(GetPromillle(), 1).ToString(CultureInfo.InvariantCulture) + "‰", textActual);
            }
            else if (promille >= 0.7 && promille < 1.4)
            {
                IWebElement outputElement = _driver.FindElement(By.Id("Promille2"));
                string textActual = outputElement.Text;
                Assert.AreEqual(Math.Round(GetPromillle(), 1).ToString(CultureInfo.InvariantCulture) + "‰", textActual);
            }
            else if (promille >= 1.4 && promille < 2.9)
            {
                IWebElement outputElement = _driver.FindElement(By.Id("Promille3"));
                string textActual = outputElement.Text;
                Assert.AreEqual(Math.Round(GetPromillle(), 1).ToString(CultureInfo.InvariantCulture) + "‰", textActual);
            }
            else
            {
                IWebElement outputElement = _driver.FindElement(By.Id("Promille4"));
                string textActual = outputElement.Text;
                Assert.AreEqual(Math.Round(GetPromillle(), 1).ToString(CultureInfo.InvariantCulture) + "‰", textActual);
            }
        }

        [TestMethod]
        public void GetListTest()
        {
            IWebElement showAllElement = _driver.FindElement(By.Id("showAllButton"));
            showAllElement.Click();

            Thread.Sleep(2000);

            IWebElement outputElement = _driver.FindElement(By.Id("DrinkList"));
            string text = outputElement.Text;
            Assert.IsTrue(text.Contains("GG"));
            Assert.IsTrue(text.Contains("Promille Change"));
        }

        [TestMethod]
        public void GetModalTest()
        {
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
            Assert.AreEqual("GG", modalDrinkName);
            Assert.IsTrue(modalIngredients.Contains("Galliano"));
            Assert.AreEqual("Alcohol 42%", modalAlcohol);
            Assert.IsTrue(modalMeasurements.Contains("2 1/2"));
        }

        [TestMethod]
        public void FilterTest()
        {
            IWebElement filter = _driver.FindElement(By.Id("filterButton"));
            filter.Click();

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

            IWebElement vodkaElement = _driver.FindElement(By.Id("filterItem"));
            vodkaElement.Click();

            IWebElement maxPromille = _driver.FindElement(By.Id("alkoFilter2"));
            maxPromille.Click();

            IWebElement recommendElement = _driver.FindElement(By.Id("recommendButton"));
            recommendElement.Click();

            IWebElement outputElement = _driver.FindElement(By.Id("DrinkList"));
            string text = outputElement.Text;
            Assert.IsFalse(text.Contains("GG"));

            IWebElement outputElement2 = _driver.FindElement(By.Id("DrinkList"));
            string text2 = outputElement2.Text;
            Assert.IsTrue(text2.Contains("AT&T"));

            IWebElement reset = _driver.FindElement(By.Id("resetButton"));
            reset.Click();

            IWebElement notFilter = _driver.FindElement(By.Id("filterNotItem"));
            notFilter.Click();

            Thread.Sleep(5000);

            recommendElement.Click();

            Thread.Sleep(4000);

            IWebElement outputElement3 = _driver.FindElement(By.Id("DrinkList"));
            string text3 = outputElement3.Text;
            Assert.IsTrue(text3.Contains("Adam"));

        }

        //[TestMethod]
        //public void FilterTest2()
        //{
        //    IWebElement outputElement3 = _driver.FindElement(By.Id("DrinkList"));
        //    string text3 = outputElement3.Text;
        //    Assert.IsTrue(text3.Contains("GG"));
        //}
    }
}
