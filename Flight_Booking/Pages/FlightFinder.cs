using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;

namespace FlightBook
{
    class FlightFinder : FlightBook_Base
    {
        public FlightFinder(RemoteWebDriver browser)
        {
            _driver = browser;
            PageFactory.InitElements(browser, this);
        }

        [FindsBy(How = How.XPath, Using = ".//input[@value='oneway']")]
        public IWebElement JourneyType { get; set; }

        [FindsBy(How = How.Name, Using = "fromPort")]
        public IWebElement DepartFrom { get; set; }

        [FindsBy(How = How.Name, Using = "toPort")]
        public IWebElement ArriveAt { get; set; }

        [FindsBy(How = How.XPath, Using = ".//input[@value='First']")]
        public IWebElement ServiceClass { get; set; }

        [FindsBy(How = How.Name, Using = "findFlights")]
        public IWebElement ContinueBtn { get; set; }

        //declare methods
        //enter flight detials and click continue and assert for select flight page
        public void EnterFlightDetails(string from, string to)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(60));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name(DepartFrom.GetAttribute("Name"))));

            JourneyType.Click();
            SelectElement departfrom = new SelectElement(DepartFrom);
            departfrom.SelectByValue(from);
            SelectElement arriveat = new SelectElement(ArriveAt);
            arriveat.SelectByValue(to);
            ServiceClass.Click();
            ContinueBtn.Click();
            //Wait for next page load and assert for title of that page
            wait.Until(ExpectedConditions.TitleContains("Select a Flight"));
            Assert.That(_driver.Title, Contains.Substring("Select a Flight"));
        }        
    }
}
