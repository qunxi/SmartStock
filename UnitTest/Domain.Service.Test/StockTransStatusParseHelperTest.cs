using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain.Model.Stocks;
using Domain.Service.Crawl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Service.Test
{
    /// <summary>
    /// Summary description for SinaStockParseTest
    /// </summary>
    [TestClass]
    [DeploymentItem("stockhistory_data.htm")]
    public class StockTransStatusParseHelperTest
    {
        private readonly string htmlContent;
        
        public StockTransStatusParseHelperTest()
        {
            const string testfile = "stockhistory_data.htm";
            StreamReader readStream = new StreamReader(testfile, System.Text.Encoding.GetEncoding("GBK"));
            htmlContent = readStream.ReadToEnd();
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Test_GenerateStockStatus_ZXZQ()
        {
            IEnumerable<TransactionStatus> actualStocks = StockTransStatusParseHelper.GenerateStockStatus(htmlContent);
            List<TransactionStatus> expectStocks = new List<TransactionStatus>
            {
                new TransactionStatus("中信证券", "600030",
                    11.450, 11.540, 11.460, 11.410,
                    58354048, 669372800,
                    new DateTime(2014, 6, 30)),

                new TransactionStatus("中信证券", "600030",
                    11.400, 11.450, 11.420, 11.310,
                    44497736, 506808320,
                    new DateTime(2014, 6, 27)),

                new TransactionStatus("中信证券", "600030",
                    11.280, 11.460, 11.400, 11.280,
                    57277592, 653931008,
                    new DateTime(2014, 6, 26)),
            };

            bool isEqual = !expectStocks.Where((t, i) => !actualStocks.ElementAt(i).Equals(t)).Any();
            Assert.IsTrue(isEqual);
        }
    }
}
