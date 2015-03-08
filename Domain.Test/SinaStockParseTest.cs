using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Model.Test
{
    /// <summary>
    /// Summary description for SinaStockParseTest
    /// </summary>
    [TestClass]
    [DeploymentItem("stockhistory_data.htm")]
    public class SinaStockParseTest
    {
        private readonly string _htmlContent;
        
        public SinaStockParseTest()
        {
            const string testfile = "stockhistory_data.htm";
            StreamReader readStream = new StreamReader(testfile, System.Text.Encoding.GetEncoding("GBK"));
            _htmlContent = readStream.ReadToEnd();
        }

        private TestContext _testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return _testContextInstance;
            }
            set
            {
                _testContextInstance = value;
            }
        }

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
            //SinaStockParse parser = new SinaStockParse(_htmlContent);
            IEnumerable<StockTransactionStatus> actualStocks = SinaStockTransStatusParse.GenerateStockStatus(_htmlContent);
            List<StockTransactionStatus> expectStocks = new List<StockTransactionStatus>
            {
                new StockTransactionStatus("中信证券", "600030",
                    11.450, 11.540, 11.460, 11.410,
                    58354048, 669372800,
                    new DateTime(2014, 6, 30)),

                new StockTransactionStatus("中信证券", "600030",
                    11.400, 11.450, 11.420, 11.310,
                    44497736, 506808320,
                    new DateTime(2014, 6, 27)),

                new StockTransactionStatus("中信证券", "600030",
                    11.280, 11.460, 11.400, 11.280,
                    57277592, 653931008,
                    new DateTime(2014, 6, 26)),
            };

            bool isEqual = !expectStocks.Where((t, i) => actualStocks.ElementAt(i) != t).Any();
            Assert.IsTrue(isEqual);
        }


        [TestMethod]
        public void Test_GenerateStock_ZXZQ()
        {
            Stock actualStock = SinaStockTransStatusParse.GenerateStock(this._htmlContent);
            Stock expectStock = new Stock("中信证券", "600030");
            expectStock.AddTransactionStatus(
                new StockTransactionStatus("中信证券", "600030", 11.450, 11.540, 11.460, 11.410, 58354048, 669372800, new DateTime(2014, 6, 30)));
            expectStock.AddTransactionStatus(
                new StockTransactionStatus("中信证券", "600030", 11.400, 11.450, 11.420, 11.310, 44497736, 506808320, new DateTime(2014, 6, 27)));
            expectStock.AddTransactionStatus(
                new StockTransactionStatus("中信证券", "600030", 11.280, 11.460, 11.400, 11.280, 57277592, 653931008, new DateTime(2014, 6, 26)));
           
            bool isEqual = expectStock.Equals(actualStock) 
                           && !expectStock.TransactionStatus.Where((t, i) => !actualStock.TransactionStatus.ElementAt(i).Equals(t)).Any();
            Assert.IsTrue(isEqual);
        }
    }
}
