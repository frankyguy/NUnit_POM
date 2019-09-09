using NUnit.Framework;

namespace FlightBook
{
    [TestFixture]
    [Parallelizable]
    public class BookFlightTest : FlightBook_Base
    {
        [Test]
        [Parallelizable]
        [TestCaseSource(typeof(FlightBook_Base), "BrowserToRunWith")]
        [Category("BookaFlight")]
        public void BookFLightsTest(string BrowserName)
        {
            //intialise test report
            test = extent.StartTest(TestContext.CurrentContext.Test.Name);
            //Select the browser to the test
            Setup(BrowserName);

            /***Test Start***/
            //intialise login page
            LoginPage loginPg = new LoginPage(_driver);
            //Launch the url and enter the user credentials and click submit
            loginPg.Navigate();
            takeScreenshot("loginPage1", "Login Page: ", "pass");
            loginPg.login("mercury", "mercury");
            takeScreenshot("loginPage", "Login Successfull: ", "pass");

            //intialise Flight Finder page
            FlightFinder Ffinder = new FlightFinder(_driver);
            //Enter Flight Details and click continue
            Ffinder.EnterFlightDetails("Sydney", "London");
            takeScreenshot("FlightFinder", "Flight Finder Successfull: ", "pass");

            //intialise Flight Select page
            FlightSelect FSelect = new FlightSelect(_driver);
            //Select the flight and click continue
            FSelect.SelectFlight();
            takeScreenshot("FlightSelect", "Flight Select Successfull: ", "pass");

            //intialise Flight Booking page
            BookFlight FBook = new BookFlight(_driver);
            //Enter the booking details along with credit card number and click purchase button
            FBook.BookFlights("Francis", "Manoharan", "1111222233334444");
            takeScreenshot("FlightBook", "Flight Booking Successfull: ", "pass");
            /***Test End***/
        }
    }
}
