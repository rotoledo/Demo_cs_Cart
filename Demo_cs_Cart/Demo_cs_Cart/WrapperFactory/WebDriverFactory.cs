using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;

namespace Demo_cs_Cart.WrapperFactory
{
    class WebDriverFactory
    {
        private static readonly IDictionary<string, IWebDriver> Drivers = new Dictionary<string, IWebDriver>();
        private static IWebDriver driver;

        public static IWebDriver Driver
        {
            get { return driver; }
            private set { driver = value; }
        }

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

        public static void LoadApplication(string url)
        {
            Driver.Url = url;
        }

        public static void CloseAllDrivers()
        {
            foreach (var key in Drivers.Keys)
            {
                Drivers[key].Close();
                Drivers[key].Quit();
            }
        }
    }
}
