using Dapper;
using NUnit.Framework;
using System;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;

namespace Demo_cs_Cart.TestDataAccess
{
    class ExcelDataAccess
    {
        static String testSuite;
        static String testCase;

        public static void InitiateData()
        {
            testSuite = TestContext.CurrentContext.Test.Properties.Get("Category").ToString();
            testCase = TestContext.CurrentContext.Test.Properties.Get("ClassName").ToString();
        }

        public static string TestDataFileConnection()
        {
            InitiateData();
            var fileName = ConfigurationManager.AppSettings["TestDataSheetPath"];
            var con = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = {0}; Extended Properties=Excel 12.0;", fileName);
            return con;
        }

        public static EntityData GetTestData()
        {
            using (var connection = new OleDbConnection(TestDataFileConnection()))
            {
                InitiateData();
                connection.Open();
                var query = string.Format("select * from [" + testSuite + "$] where CT='{0}'", testCase);
                var value = connection.Query<EntityData>(query).FirstOrDefault();
                connection.Close();
                return value;
            }
        }


        public static void SetTestData(string column, String data)
        {
            using (var connection = new OleDbConnection(TestDataFileConnection()))
            {
                InitiateData();
                connection.Open();
                var query = string.Format("update [" + testSuite + "$] set " + column + "='" + data + "' where CT='{0}'", testCase);
                connection.Execute(query);
                connection.Close();
            }
        }

        public static void SetTestDataAll(string column, String data)
        {
            using (var connection = new OleDbConnection(TestDataFileConnection()))
            {
                InitiateData();
                connection.Open();
                var query = "update [" + testSuite + "$] set " + column + "='" + data + "'";
                connection.Execute(query);
                connection.Close();
            }
        }
    }
}
