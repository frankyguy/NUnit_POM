using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;

namespace FlightBook
{
    class LoginPage : FlightBook_Base
    {
        public LoginPage(RemoteWebDriver browser)
        {
            _driver = browser;
            PageFactory.InitElements(browser, this);
        }

        [FindsBy(How = How.Name, Using = "userName")]
        public IWebElement UserName { get; set; }

        [FindsBy(How = How.Name, Using = "password")]
        public IWebElement Password { get; set; }

        [FindsBy(How = How.Name, Using = "login")]
        public IWebElement SubmitButton { get; set; }

        //declare methods
        //navigate to the url and assert for the login pagetitle
        public void Navigate()
        {            
            _driver.Navigate().GoToUrl(url);            
            Assert.That(_driver.Title, Contains.Substring("Sign-on"));
        }
        
        //login and assert login is successfull
        public void login(string uname, string password)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(60));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name(UserName.GetAttribute("Name"))));           

            UserName.Clear();
            UserName.SendKeys(uname);
            Password.Clear();
            Password.SendKeys(password);
            SubmitButton.Click();
            //Wait for next page load and assert for title of that page
            wait.Until(ExpectedConditions.TitleContains("Find a Flight"));
            Assert.That(_driver.Title, Contains.Substring("Find a Flight"));
            
        }
    }
}
