using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using Demo_cs_Cart.WrapperFactory;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Demo_cs_Cart.Support;
using Demo_cs_Cart.TestDataAccess;
using System;
using Demo_cs_Cart.PageObjects;
using System.Threading;
using Demo_cs_Cart.Demo_cs_CartException;

namespace Demo_cs_Cart
{
    [TestFixture]
    [Category("DEMO_STORE_PANEL")]
    public class UnitTest1
    {
        static Actions action;
        static GeneralMethods generalMethods;
        static private readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static WebDriverWait wait_30s;
        static EntityData entityData;


        [SetUp]
        public void SetUp()
        {
            // DADOS DO TESTE
            TestContext.CurrentContext.Test.Properties.Add("Category", "DEMO_STORE_PANEL");
            TestContext.CurrentContext.Test.Properties.Add("ClassName", this.GetType().Name);
            TestContext.CurrentContext.Test.Properties.Add("StepCount", 1);
            entityData = ExcelDataAccess.GetTestData();

            // OBJETOS DO TESTE
            WebDriverFactory.InitBrowser("Chrome");
            generalMethods = new GeneralMethods();
            action = new Actions(WebDriverFactory.Driver);
            wait_30s = new WebDriverWait(WebDriverFactory.Driver, TimeSpan.FromSeconds(30));
            WebDriverFactory.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
        }


        [Test]
        public void TestMethod1()
        {
            // Acessa Homepage
            generalMethods.AcessPage(entityData);

            // Busca 01 - Batman
            generalMethods.SearchForProduct(entityData.Search01);

            // Busca 02 - PS3
            generalMethods.SearchForProduct(entityData.Search02);

            // Valida Produtos no Carrinho
            Thread.Sleep(5000);
            Page.Login.Icon_Carrinho.Click();
            try
            {
                Assert.True(Page.Login.List_Carrinho.Text.Contains(entityData.Search01), "Carrinho não contém produto: " + entityData.Search01);
                Assert.True(Page.Login.List_Carrinho.Text.Contains(entityData.Search02), "Carrinho não contém produto: " + entityData.Search02);
            }
            catch (AssertionException e)
            { throw new TestFailedException(e.Message); }
            generalMethods.printAndLog("Validação OK - Produtos no Carrinho");

            // OK > Teste executado com Sucesso
            generalMethods.printAndLog("OK - Teste executado com Sucesso");
        }


        [TearDown]
        public void TearDown()
        {
            generalMethods.tearDown();
        }
    }
}
