using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;

namespace FlightBook
{
    class FlightSelect : FlightBook_Base
    {
        public FlightSelect(RemoteWebDriver browser)
        {
            _driver = browser;
            PageFactory.InitElements(browser, this);
        }

        [FindsBy(How = How.Name, Using = "reserveFlights")]
        public IWebElement ContinueBtn2 { get; set; }

        //declare methods
        //Select flight and click continue and assert for select book a flight page
        public void SelectFlight()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(60));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name(ContinueBtn2.GetAttribute("Name"))));

            ContinueBtn2.Click();
            wait.Until(ExpectedConditions.TitleContains("Book a Flight"));
            //Wait for next page load and assert for title of that page
            Assert.That(_driver.Title, Contains.Substring("Book a Flight"));

        }        
    }
}
