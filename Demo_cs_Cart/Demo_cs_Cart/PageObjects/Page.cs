using OpenQA.Selenium.Support.PageObjects;
using Demo_cs_Cart.WrapperFactory;

namespace Demo_cs_Cart.PageObjects
{

    /// <summary>
    /// PageObject Design Pattern - Easily instantiate and maintain the WebElement instance
    /// </summary>
    class Page
    {

        private static T GetPage<T>() where T : new()
        {
            var page = new T();
            PageFactory.InitElements(WebDriverFactory.Driver, page);
            return page;
        }

        /// <summary>
        /// PageObject Design Pattern. Get the page you want
        /// </summary>
        public static Demo_cs_Cart Demo_cs_Cart
        {
            get { return GetPage<Demo_cs_Cart>(); }
        }

    }
}