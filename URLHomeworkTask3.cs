using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace URLShortener_HW_Task3
{
    public class Tests
    {
        IWebDriver driver;
        IWebElement HomeButton;
        IWebElement ShortsURLsButton;
        IWebElement AddURLsButton;


        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = "https://shorturl.nakov.repl.co";
            HomeButton = driver.FindElement(By.CssSelector("body > header > a:nth-child(1)"));
            ShortsURLsButton = driver.FindElement(By.CssSelector("body > header > a:nth-child(3)"));
            AddURLsButton = driver.FindElement(By.CssSelector("body > header > a:nth-child(5)"));
        }
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            
        }

        [Test]
        public void Test_HomePage()
        {
            var title = driver.FindElement(By.CssSelector("body > main > h1")).Text;
            Assert.That("URL Shortener", Is.EqualTo(title));
        }
        [Test]
        public void Test_ShortsURLs()
        {
            ShortsURLsButton.Click();
            var title = driver.FindElement(By.CssSelector("body > main > h1")).Text;
            Assert.That("Short URLs", Is.EqualTo(title));
            var firstCell = driver.FindElement(By.CssSelector("body > main > table > tbody > tr:nth-child(1) > td:nth-child(1) > a")).Text;
            var secondCell = driver.FindElement(By.CssSelector("body > main > table > tbody > tr:nth-child(2) > td:nth-child(1) > a")).Text;
            var thirtCell = driver.FindElement(By.CssSelector("body > main > table > tbody > tr:nth-child(3) > td:nth-child(1) > a")).Text;
            Assert.That("https://nakov.com", Is.EqualTo(firstCell));
            Assert.That("https://selenium.dev", Is.EqualTo(secondCell));
            Assert.That("https://nodejs.org", Is.EqualTo(thirtCell));
            var firstCellShortURL = driver.FindElement(By.CssSelector("body > main > table > tbody > tr:nth-child(1) > td:nth-child(2) > a")).Text;
            var secondCellShortURL = driver.FindElement(By.CssSelector("body > main > table > tbody > tr:nth-child(2) > td:nth-child(2) > a")).Text;
            var thirdCellShortURL = driver.FindElement(By.CssSelector("body > main > table > tbody > tr:nth-child(3) > td:nth-child(2) > a")).Text;
            Assert.That("http://shorturl.nakov.repl.co/go/nak", Is.EqualTo(firstCellShortURL));
            Assert.That("http://shorturl.nakov.repl.co/go/seldev", Is.EqualTo(secondCellShortURL));
            Assert.That("http://shorturl.nakov.repl.co/go/node", Is.EqualTo(thirdCellShortURL));

        }
        [Test]
        public void Test_AddInvalidURL()
        {
            AddURLsButton.Click();
            var title = driver.FindElement(By.CssSelector("body > main > h1")).Text;
            Assert.That("Add Short URL", Is.EqualTo(title));
            var URLButton = driver.FindElement(By.CssSelector("#url"));
            URLButton.SendKeys("1");
            var CreateButton = driver.FindElement(By.CssSelector("body > main > form > table > tbody > tr:nth-child(3) > td > button"));
            CreateButton.Click();
            var resultMassage = driver.FindElement(By.CssSelector("body > div")).Text;
            Assert.That("Invalid URL!", Is.EqualTo(resultMassage));
        }
        [Test]
        public void Test_AddValidURL()
        {
            AddURLsButton.Click();
            var title = driver.FindElement(By.CssSelector("body > main > h1")).Text;
            Assert.That("Add Short URL", Is.EqualTo(title));
            var URLButton = driver.FindElement(By.CssSelector("#url"));
            URLButton.SendKeys("https://universe.smarty-kids.eu/");
            var CreateButton = driver.FindElement(By.CssSelector("body > main > form > table > tbody > tr:nth-child(3) > td > button"));
            CreateButton.Click();
            ShortsURLsButton.Click();

        }
    }
}