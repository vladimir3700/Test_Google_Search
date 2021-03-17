using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Linq;

namespace Test_Google_Search
{
    
    public class Tests
    {
        IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            var options = new OpenQA.Selenium.Chrome.ChromeOptions();


            //options.AddArgument("--lang=locale-of-choice");
            options.AddArgument("--lang=ru");
            options.AddArgument("--cr=countryRU");
        

            options.BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
            driver = new OpenQA.Selenium.Chrome.ChromeDriver(options);
            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl("https://www.google.com/preferences?hl=ru-UA&fg=1");

            IWebElement click_slower = driver.FindElement(By.Id("result_slider"));
            click_slower.Click();

            IWebElement click_more = driver.FindElement(By.Id("regionanchormore"));
            click_more.Click();

            IWebElement choose_region = driver.FindElement(By.Id("regionoRU"));
            choose_region.Click();

            Thread.Sleep(1000);

            By click_save_button = By.XPath("//div[@class='goog-inline-block jfk-button jfk-button-action']");
            var clicking_save_button = driver.FindElement(click_save_button);
            clicking_save_button.Click();

        }

        [Test]
        public void Test1()
        {
            Thread.Sleep(4000);

            //1) Type text
            IWebElement type_search = driver.FindElement(By.Name("q"));
            string search_text = "купить кофемашину bork c804";
            type_search.SendKeys(search_text);

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.Name("btnK")));

            //2) Click the "Search" button
            Thread.Sleep(1000);

            
          
            IWebElement click_search = driver.FindElement(By.Name("btnK"));
            click_search.Click();

            //3) Get results
            IWebElement results_google = driver.FindElement(By.Id("search"));


            ReadOnlyCollection<IWebElement> results_search_google = results_google.FindElements(By.XPath(".//a"));

            foreach (IWebElement result in results_search_google)
            {
                Console.WriteLine(result.Text);
            }

           

            //Assert.Pass();
        }

        [TearDown]

        public void TearDown()
        {
            Thread.Sleep(10000);
            driver.Close();
        }
    }
}