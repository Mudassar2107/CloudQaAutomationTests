using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace CloudQaAutomationTests
{
    [TestFixture]
    public class AutomationPracticeFormTests
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private const string FormUrl = "https://app.cloudqa.io/home/AutomationPracticeForm";

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(FormUrl);

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void FirstNameField_AllowsEnteringText()
        {
            var firstNameLocator = By.XPath("//label[normalize-space()='First Name']/following::input[1]");
            var firstNameInput = wait.Until(d => d.FindElement(firstNameLocator));

            var expectedName = "Mudassar";
            firstNameInput.Clear();
            firstNameInput.SendKeys(expectedName);

            var actual = firstNameInput.GetAttribute("value");
            if (!string.Equals(expectedName, actual))
            {
                throw new Exception($"Expected first name '{expectedName}' but found '{actual}'.");
            }
        }

        [Test]
        public void Gender_Male_CanBeSelected()
        {
            var maleRadioLocator = By.XPath("//input[@type='radio' and @value='Male']");
            var maleRadio = wait.Until(d => d.FindElement(maleRadioLocator));

            if (!maleRadio.Selected)
            {
                maleRadio.Click();
            }

            if (!maleRadio.Selected)
            {
                throw new Exception("Male radio button was not selected.");
            }
        }

        [Test]
        public void CountryDropdown_CanSelectIndia()
        {
            var countrySelectLocator = By.XPath(
                "//label[normalize-space()='Country' or normalize-space()=' Country']/following::select[1]"
            );
            var countrySelectElement = wait.Until(d => d.FindElement(countrySelectLocator));
            var select = new SelectElement(countrySelectElement);

            var expectedCountry = "India";
            select.SelectByText(expectedCountry);

            var actualCountry = select.SelectedOption.Text;
            if (!string.Equals(expectedCountry, actualCountry))
            {
                throw new Exception($"Expected selected country '{expectedCountry}' but found '{actualCountry}'.");
            }
        }
    }
}
