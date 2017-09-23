using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using Demo_cs_Cart.WrapperFactory;
using System;

namespace Demo_cs_Cart.PageObjects
{
    class Demo_cs_Cart
    {
        [FindsBy(How = How.CssSelector, Using = "input#search_input")]
        [CacheLookup]
        public IWebElement Input_SearchBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div#pagination_contents div:nth-child(2) div")]
        [CacheLookup]
        public IWebElement Grid_Item01 { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button[id^=button_cart_")]
        [CacheLookup]
        public IWebElement Button_AdicionarAoCarrinho { get; set; }

        [FindsBy(How = How.CssSelector, Using = "img[src$='cart_r22q-n3.png']")]
        [CacheLookup]
        public IWebElement Button_HomePage { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div#sw_dropdown_8")]
        [CacheLookup]
        public IWebElement Icon_Carrinho { get; set; }

        [FindsBy(How = How.CssSelector, Using = "ul.ty-cart-items__list")]
        [CacheLookup]
        public IWebElement List_Carrinho { get; set; }
    }
}
