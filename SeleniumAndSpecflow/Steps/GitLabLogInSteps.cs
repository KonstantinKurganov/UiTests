using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using SeleniumAndSpecflow.Pages;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SeleniumAndSpecflow
{
    [Binding]
    public class GitLabLogInSteps : IDisposable
    {
        private readonly IWebDriver webDriver;
        private readonly IWait<IWebDriver> defaultWait;
        private Credentials credentials;
        private LoginPage page;

        public GitLabLogInSteps()
        {
            webDriver = new ChromeDriver();
            defaultWait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            credentials = new Credentials();
            page = new LoginPage(webDriver);
            page.UpdateElements();
        }


        [Given(@"I navigate to (.*)")]
        public void WhenINavigateToGitlab(string url)
        {
            webDriver.Navigate().GoToUrl(url);
        }
        
        [When(@"I fill log in form with valid credentials")]
        [When(@"I fill log in form with invalid credentials")]
        public void WhenIEnterMyValidCredantials(Table table)
        {
            credentials = table.CreateInstance<Credentials>();
            
            page.LoginField.SendKeys(credentials.Username);
            page.PasswordField.SendKeys(credentials.Password);

        }
        
        [When(@"I press login button")]
        public void WhenIPressLoginButton()
        {
            page.LogInBtn.Click();
        }
        
        [Then(@"I should be thrown to main page")]
        public void ThenIShouldBeThrownToMainPage()
        {
            
            var curUrl = webDriver.Url;
            Assert.AreEqual(curUrl, "https://gitlab.com/");
        }

        [Then(@"I should see red alert box")]
        public void ThenIShouldSeeRedAlertBox()
        {
            WaitSec(5);
            page.UpdateElements();
            Assert.IsNotNull(page.Alert);

        }

        [When(@"I leave login form blank")]
        public void WhenILeaveLoginFormBlank()
        {
            
        }

        [Then(@"I should see warning message under blank fields")]
        public void ThenIShouldSeeWarningMessageUnderBlankFields()
        {
            WaitSec(5);
            page.UpdateElements();
            Assert.IsNotNull(page.WarningMessage);
        }

        [When(@"I clicked Remember me check box")]
        public void WhenIClickedRememberMeCheckBox()
        {
            page.RememberMeCheckBox.Click();
        }

        [Then(@"After reopening browser I stay logged in")]
        public void ThenAfterReopeningBrowserIStayLoggedIn()
        {
            ReopenBrowser();
            WaitSec(5);
            Assert.AreEqual(webDriver.Url, "https://gitlab.com/");

        }
        [Then(@"After reopening browser I see about page")]
        public void ThenAfterReopeningBrowserISeeAboutPage()
        {
            ReopenBrowser();
            WaitSec(5);
            Assert.AreEqual(webDriver.Url, "https://about.gitlab.com/");

        }


        public void ReopenBrowser()
        {
            var cookies = webDriver.Manage().Cookies.AllCookies;
            foreach (var c in cookies)
            {
                // Simulate a browser restart by removing all non-persistent cookies.
                if (c.Expiry == null)
                    webDriver.Manage().Cookies.DeleteCookie(c);
            }
            webDriver.Navigate().Refresh();
        }

        public void WaitSec(int sec)
        {
            webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(sec);
        }

        public void Dispose()
        {
            webDriver.Quit();
        }
    }

    public class Credentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
