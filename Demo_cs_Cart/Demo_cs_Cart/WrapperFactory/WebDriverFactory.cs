using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.Collections.Generic;

namespace Demo_cs_Cart.WrapperFactory
{

    /// <summary>
    /// Factory Design Pattern. Easily instantiate and maintain the WebDriver instance
    /// </summary>
    class WebDriverFactory
    {
        private static readonly IDictionary<string, IWebDriver> Drivers = new Dictionary<string, IWebDriver>();
        private static IWebDriver driver;

        public static IWebDriver Driver
        {
            get { return driver; }
            private set { driver = value; }
        }

        /// <summary>
        /// Factory Design Pattern. Initiate the WebDriver
        /// </summary>
        /// <param name="browserName"></param>
        public static void InitBrowser(string browserName)
        {
            driver = null;

            switch (browserName)
            {
                case "Firefox":
                    driver = new FirefoxDriver();
                    break;

                case "IE":
                    driver = new InternetExplorerDriver();
                    break;

                case "Chrome":
                    driver = new ChromeDriver();
                    break;
            }
        }
    }
}
