using OpenQA.Selenium.Support.PageObjects;
using Demo_cs_Cart.WrapperFactory;

namespace Demo_cs_Cart.PageObjects
{
    class Page
    {

        private static T GetPage<T>() where T : new()
        {
            var page = new T();
            PageFactory.InitElements(WebDriverFactory.Driver, page);
            return page;
        }

        public static Demo_cs_Cart Demo_cs_Cart
        {
            get { return GetPage<Demo_cs_Cart>(); }
        }

    }
}