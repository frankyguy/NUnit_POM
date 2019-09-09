using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;

namespace FlightBook
{
    class BookFlight : FlightBook_Base
    {
        public BookFlight(RemoteWebDriver browser)
        {
            _driver = browser;
            PageFactory.InitElements(browser, this);
        }

        [FindsBy(How = How.Name, Using = "passFirst0")]
        public IWebElement FirstName { get; set; }

        [FindsBy(How = How.Name, Using = "passLast0")]
        public IWebElement LastName { get; set; }

        [FindsBy(How = How.Name, Using = "creditnumber")]
        public IWebElement CreditCard { get; set; }

        [FindsBy(How = How.Name, Using = "ticketLess")]
        public IWebElement TicketLess { get; set; }

        [FindsBy(How = How.Name, Using = "buyFlights")]
        public IWebElement PurchaseBtn { get; set; }

        //declare methods
        //Book Flight and click continue and assert for Flight Confiramtion Page
        public void BookFlights(string fname, string lname, string creditcard)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(60));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name(FirstName.GetAttribute("Name"))));
                        
            FirstName.SendKeys(fname);
            LastName.SendKeys(lname);
            CreditCard.SendKeys(creditcard);
            TicketLess.Click();
            PurchaseBtn.Click();
            //Wait for next page load and assert for title of that page
            wait.Until(ExpectedConditions.TitleContains("Flight Confirmation"));
            Assert.That(_driver.Title, Contains.Substring("Flight Confirmation"));

        }        
    }
}
