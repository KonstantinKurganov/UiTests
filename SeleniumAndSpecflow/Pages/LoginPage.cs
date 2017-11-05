using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAndSpecflow.Pages
{
    public class LoginPage
    {
        private IWebDriver webDriver;

        [FindsBy(How = How.Id, Using = "user_login")]
        public IWebElement LoginField { get; set; }

        [FindsBy(How = How.Id, Using = "user_password")]
        public IWebElement PasswordField { get; set; }

        [FindsBy(How = How.Name, Using = "commit")]
        public IWebElement LogInBtn { get; set; }

        [FindsBy(How = How.ClassName, Using = "flash - alert")]
        public IWebElement Alert { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#new_user > div:nth-child(3) > p")]
        public IWebElement WarningMessage { get; set; }

        [FindsBy(How = How.Id, Using = "user_remember_me")]
        public IWebElement RememberMeCheckBox { get; set; }

        

        public LoginPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        public void UpdateElements()
        {
            PageFactory.InitElements(this, new RetryingElementLocator(webDriver, TimeSpan.FromSeconds(10)));

        }


    }
}
