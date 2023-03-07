using System;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace SpecFlowSelenium.PageObjects
{
    public class ChorusPageObjects
    {
        /// <summary>
        /// Calculator Page Object
        /// </summary>
 //The URL of the calculator to be opened in the browser
        private const string ChorusUrl = "https://www.chorus.co.nz/tools-support/broadband-tools/broadband-map";

        //The Selenium web driver to automate the browser
        private readonly IWebDriver _webDriver;

        //The default wait time in seconds for wait.Until
        public const int DefaultWaitInSeconds = 5;

        public ChorusPageObjects(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        //Finding elements by ID
        private IWebElement AddressElement => WaitUntil(()=> _webDriver.FindElement(By.Id("wialus-search-input")), result=>result.Displayed);
        private IWebElement listedAddress => WaitUntil(()=> _webDriver.FindElement(By.ClassName("selected")), result => result.Displayed);
        private IWebElement SearchIconElement => _webDriver.FindElement(By.ClassName("address-search-icon"));
        private IWebElement ResultElement => _webDriver.FindElement(By.ClassName("location-details-now"));

        public void EnterAddress(string address)
        {
            //Clear text box
            AddressElement.Clear();
            //Enter text
            AddressElement.SendKeys(address);
        }

        public void selectAddress()
        {
            listedAddress.Click();
        }

        public void ClickSearch()
        {
            //Click the add button
            SearchIconElement.Click();
        }

        public void EnsureChorusAppIsOpen()
        {
            //Open the chorus page in the browser if not opened yet
            if (_webDriver.Url != ChorusUrl)
            {
                _webDriver.Url = ChorusUrl;
            }
        }

        public string WaitForNonEmptyResult(string txt)
        {
            //Wait for the result to be not empty
            return WaitUntil(
                () => ResultElement.FindElement(By.XPath(String.Format("//li[contains(text(), '{0}')]", txt))).Text,
                result => !string.IsNullOrEmpty(result));
        }

        /// <summary>
        /// Helper method to wait until the expected result is available on the UI
        /// </summary>
        /// <typeparam name="T">The type of result to retrieve</typeparam>
        /// <param name="getResult">The function to poll the result from the UI</param>
        /// <param name="isResultAccepted">The function to decide if the polled result is accepted</param>
        /// <returns>An accepted result returned from the UI. If the UI does not return an accepted result within the timeout an exception is thrown.</returns>
        private T WaitUntil<T>(Func<T> getResult, Func<T, bool> isResultAccepted) where T : class
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(DefaultWaitInSeconds));
            return wait.Until(driver =>
            {
                var result = getResult();
                if (!isResultAccepted(result))
                    return default;

                return result;
            });

        }
    }
}
