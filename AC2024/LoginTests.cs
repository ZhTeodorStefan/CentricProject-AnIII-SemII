using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;

namespace AC2024
{
    [TestClass]
    public class LoginTests
    {
        private IWebDriver driver;

        [TestInitialize]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("incognito");
            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
        }

        [TestMethod]
        public void LogIn()
        {
            driver.Navigate().GoToUrl("https://bstackdemo.com/");

            // Wait for the sign-in button to be clickable and then click it
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement signInButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("signin")));
            signInButton.Click();

            // Check if the input is within an iframe
            var iframes = driver.FindElements(By.TagName("iframe"));
            foreach (var iframe in iframes)
            {
                driver.SwitchTo().Frame(iframe);
                if (IsElementPresent(By.CssSelector("#react-select-2-input")))
                {
                    break;
                }
                driver.SwitchTo().DefaultContent();
            }

            // Wait for the input element to be present and visible
            IWebElement inputElement = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#react-select-2-input")));

            // Enter the desired value into the input
            inputElement.SendKeys("demouser");

            // Select the option
            inputElement.SendKeys(Keys.Enter);

            foreach (var iframe in iframes)
            {
                driver.SwitchTo().Frame(iframe);
                if (IsElementPresent(By.CssSelector("#react-select-3-input")))
                {
                    break;
                }
                driver.SwitchTo().DefaultContent();
            }

            // Wait for the input element to be present and visible
            inputElement = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#react-select-3-input")));

            // Enter the desired value into the input
            inputElement.SendKeys("testingisfun99");

            // Select the option
            inputElement.SendKeys(Keys.Enter);

            driver.FindElement(By.Id("login-btn")).Click();

            // Add verification/assertion if needed
            Console.WriteLine("Test passed");
        }

        [TestMethod]
        public void AddToCart()
        {
            LogIn();

            /*var iframes = driver.FindElements(By.TagName("iframe"));
            foreach (var iframe in iframes)
            {
                driver.SwitchTo().Frame(iframe);
                if (IsElementPresent(By.Id("2")))
                {
                    break;
                }
                driver.SwitchTo().DefaultContent();
            }*/

            //IWebElement product = driver.FindElement(By.Id("2"));
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement element = wait.Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div/div/div/main/div[2]/div[3]/div[4]")));
            element.Click();
            element.Click();

            element = wait.Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div/div/div/main/div[2]/div[2]/div[4]")));
            element.Click();


            /*IList<IWebElement> divs = product.FindElements(By.TagName("div"));

            foreach (var div in divs)
            {
                if (div.GetAttribute("class") == "shelf-item__buy-btn")
                    div.Click();
            }*/
        }

        [TestMethod]
        public void CheckOut()
        {
            AddToCart();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement element = wait.Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div/div/div/div[2]/div[2]/div[3]/div[3]")));
            element.Click();

            element = wait.Until(ExpectedConditions.ElementExists(By.Id("firstNameInput")));
            element.SendKeys("Lali");

            element = wait.Until(ExpectedConditions.ElementExists(By.Id("lastNameInput")));
            element.SendKeys("Melo");

            element = wait.Until(ExpectedConditions.ElementExists(By.Id("addressLine1Input")));
            element.SendKeys("Strada Ciobelor nr 68");

            element = wait.Until(ExpectedConditions.ElementExists(By.Id("provinceInput")));
            element.SendKeys("Iason");

            element = wait.Until(ExpectedConditions.ElementExists(By.Id("postCodeInput")));
            element.SendKeys("42069");

            element = wait.Until(ExpectedConditions.ElementExists(By.Id("checkout-shipping-continue")));
            element.Click();

            /*element = wait.Until(ExpectedConditions.ElementExists(By.ClassName("button.button.button--tertiary.optimizedCheckout-buttonSecondary")));
            element.Click();*/

            driver.Navigate().GoToUrl("https://bstackdemo.com/");
        }

        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }



        /*[TestCleanup]
        public void TearDown()
        {
            driver.Quit();
        }*/
    }
}
