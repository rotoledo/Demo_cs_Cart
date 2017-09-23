﻿using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Demo_cs_Cart.PageObjects;
using Demo_cs_Cart.WrapperFactory;
using Demo_cs_Cart.TestDataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace Demo_cs_Cart.Support
{
    class GeneralMethods
    {
        static Actions action;
        static WebDriverWait wait_30s;
        static WebDriverWait wait_5s;
        static int times = 0;
        static String testSuite;
        static String testCase;
        static private readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static String directoryLocation;
        static Stopwatch cronometro = new Stopwatch();

        public void InitiateData()
        {
            testSuite = TestContext.CurrentContext.Test.Properties.Get("Category").ToString();
            testCase = TestContext.CurrentContext.Test.Properties.Get("ClassName").ToString();
            String today = DateTime.Today.ToString("dd_MM_yyyy");
            directoryLocation = ConfigurationManager.AppSettings["Output"].ToString() + "Output - " + today + "\\" + testSuite + "\\" + testCase + "\\";
        }

        /// <summary>
        /// Acessa Page
        /// </summary>
        /// <param name="entityData"></param>
        public void AcessPage(EntityData entityData)
        {
            cronometro.Start();
            WebDriverFactory.Driver.Navigate().GoToUrl(entityData.Url);
            WebDriverFactory.Driver.Manage().Window.Maximize();
            this.printAndLog("Acessa Homepage");
        }


        /// <summary>
        /// Busca por Produto
        /// </summary>
        /// <param name="product"></param>
        public void SearchForProduct(String product)
        {
            wait_30s = new WebDriverWait(WebDriverFactory.Driver, TimeSpan.FromSeconds(30));

            // Busca
            Page.Login.Input_SearchBox.SendKeys(product);
            this.printAndLog("Busca - " + product);
            Page.Login.Input_SearchBox.SendKeys(Keys.Enter);

            // Seleciona Produto
            this.printAndLog("Seleciona Produto");
            Page.Login.Grid_Item01.Click();

            // Adiciona ao Carrinho
            Page.Login.Button_AdicionarAoCarrinho.Click();
            this.printAndLog("Adiciona ao Carrinho");

            // Confirmação Produto Adc ao Carrinho
            wait_30s.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div[class^='cm-notification-content'")));
            this.printAndLog("Confirmação - Produto " + product + " Adc ao Carrinho");
        }


        /// <summary>
        /// Print Screen
        /// </summary>
        /// <param name="snapshotName"></param>
        public void printScreen(String snapshotName)
        {
            IWebDriver driver = WebDriverFactory.Driver;
            this.InitiateData();
            int count = 0;
            String countString = TestContext.CurrentContext.Test.Properties.Get("StepCount").ToString();

            int length = countString.Length;
            if (length == 1) { countString = "0" + countString; }

            if (!Directory.Exists(directoryLocation))
                Directory.CreateDirectory(directoryLocation);

            String fileName = "Step " + countString + " - " + snapshotName + ".png";
            var path = new StringBuilder(directoryLocation);
            path.Append(fileName);

            Int32.TryParse(countString, out count);
            count++;
            TestContext.CurrentContext.Test.Properties.Set("StepCount", count);

            ITakesScreenshot screenshotDriver = driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            screenshot.SaveAsFile(path.ToString(), ScreenshotImageFormat.Png);
        }



        /// <summary>
        /// Printa e Imprime Log
        /// </summary>
        /// <param name="msg"></param>
        public void printAndLog(String msg)
        {
            IWebDriver driver = WebDriverFactory.Driver;
            this.logInfo(msg);
            this.printScreen(msg);
        }


        /// <summary>
        /// Log Info
        /// </summary>
        /// <param name="msg"></param>
        public void logInfo(String msg)
        {
            this.InitiateData();
            GlobalContext.Properties["LogFileName"] = directoryLocation + "log";
            log4net.Config.XmlConfigurator.Configure();
            msg = msg.Replace("-", ">");
            logger.Info("[" + testCase + "]" + " - " + msg);
        }


        /// <summary>
        /// Printa e Imprime Log ERROR
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="error"></param>
        public void printAndLog(String msg, Boolean error)
        {
            IWebDriver driver = WebDriverFactory.Driver;
            msg = msg.Replace(">", "-");
            msg = msg.Replace(":", "-");
            this.logError(msg);
            msg = "  ##ERRO## - Falha no CT. Vide Log";
            this.printScreen(msg);
        }

        /// <summary>
        /// Loga Contador de Tempo
        /// </summary>
        public void logTimeCounter()
        {
            this.InitiateData();
            GlobalContext.Properties["LogFileName"] = directoryLocation + "TimeCounter";
            log4net.Config.XmlConfigurator.Configure();
            TestContext.CurrentContext.Test.Properties.Get("ClassName").ToString();
            logger.Info("[" + testCase + "]" + " - " + "Tempo de execução - " + cronometro.Elapsed.ToString(@"hh\:mm\:ss\.ff"));
        }

        /// <summary>
        /// Log Erro
        /// </summary>
        /// <param name="msg"></param>
        public void logError(String msg)
        {
            this.InitiateData();
            GlobalContext.Properties["LogFileName"] = directoryLocation + "log";
            log4net.Config.XmlConfigurator.Configure();
            msg = msg.Replace("-", ">");
            logger.Error("[" + testCase + "]" + " - ##ERRO## -> " + msg);
        }

        /// <summary>
        /// Retorna horário atual
        /// </summary>
        /// <returns></returns>
        public string getCurrentTime()
        {
            String currentHour = " DO DIA " + DateTime.Now.ToString();
            Thread.Sleep(1000);
            return currentHour;
        }

        /// <summary>
        /// Termina o teste: Print, log e termina o Driver 
        /// </summary>
        public void tearDown()
        {
            IWebDriver driver = WebDriverFactory.Driver;
            this.printAndLog("Fim do CT");
            cronometro.Stop();
            this.logTimeCounter();
            cronometro.Reset();
            driver.Quit();
        }

    }

}
