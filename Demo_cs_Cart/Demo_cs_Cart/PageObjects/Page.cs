using OpenQA.Selenium.Support.PageObjects;
using Demo_cs_Cart.WrapperFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static Demo_cs_Cart Login
        {
            get { return GetPage<Demo_cs_Cart>(); }
        }

    }
}