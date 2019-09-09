using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using System.Collections.Generic;
using OpenQA.Selenium.IE;
using RelevantCodes.ExtentReports;

namespace FlightBook
{

    public class FlightBook_Base
    {
        protected RemoteWebDriver _driver;
        protected string url = "http://newtours.demoaut.com/mercurysignon.php";

        //Extent Reports
        protected ExtentReports extent;
        protected ExtentTest test;        

        [OneTimeSetUp]
        public void StartReport()
        {
            string reportPath = "C:\\FlightBook\\FlightBookReport.html";
            extent = new ExtentReports(reportPath, true);
        }
        public static IEnumerable<String> BrowserToRunWith()
        {
            String[] browsers = { "chrome", "firefox" };
            foreach (String b in browsers)
                yield return b;
        }

        public void Setup(string browserName)
        {
            if (browserName == "firefox")
                _driver = new FirefoxDriver();
            else if (browserName == "edge")
                _driver = new EdgeDriver();
            else if (browserName == "IE")
                _driver = new InternetExplorerDriver();
            else
                _driver = new ChromeDriver(); 
        }

        [TearDown]
        public void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = "<pre>" + TestContext.CurrentContext.Result.StackTrace + "</pre>";
            var errorMessage = TestContext.CurrentContext.Result.Message;
            DateTime time = DateTime.Now;
            string dateToday = "_date_" + time.ToString("yyyy-MM-dd") + "_time_" + time.ToString("HH-mm-ss");

            Console.WriteLine("Test: " + TestContext.CurrentContext.Test.Name + "is " + errorMessage);

            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
                takeScreenshot(dateToday + "error.jpg", stackTrace + errorMessage, "fail");
            else
                takeScreenshot(dateToday + "Pass.jpg", "Snapshot below: " , "pass");

            extent.EndTest(test);
            extent.Flush();
            try
            {
                _driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        //Take Screenshot and update test step status in the HTML report
        public void takeScreenshot(string filename, string stepdesc, string Result)
        {
            var ScrShot = _driver.GetScreenshot();
            ScrShot.SaveAsFile("C:\\FlightBook\\Images\\" + filename + ".jpg", ScreenshotImageFormat.Jpeg);
            
            if (Result == "pass")
                test.Log(LogStatus.Pass, stepdesc + test.AddScreenCapture("C:\\FlightBook\\Images\\" + filename + ".jpg"));
            else
                test.Log(LogStatus.Fail, stepdesc + test.AddScreenCapture("C:\\FlightBook\\Images\\" + filename + ".jpg"));
        }

    }
}

