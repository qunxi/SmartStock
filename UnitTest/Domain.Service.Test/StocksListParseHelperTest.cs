using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using Domain.Model.Stocks;
using Domain.Service.Crawl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Service.Test
{
    [TestClass]
    [DeploymentItem("stocksummary.htm")]
    [DeploymentItem("shortcut.html")]
    public class StocksListParseHelperTest
    {
        private readonly string listContent;
        private readonly string stockContent;

        public StocksListParseHelperTest()
        {
            const string listhtml = "stocksummary.htm";
            StreamReader listStream = new StreamReader(listhtml, System.Text.Encoding.GetEncoding("GBK"));
            listContent = listStream.ReadToEnd();

            const string stockhtml = "shortcut.html";
            StreamReader stockStream = new StreamReader(stockhtml, System.Text.Encoding.GetEncoding("utf-8"));
            stockContent = stockStream.ReadToEnd();
            /*const string stockhtml = "stock.html";
            StreamReader stockStream = new StreamReader(stockhtml, System.Text.Encoding.GetEncoding("GBK"));
            stockContent = stockStream.ReadToEnd();*/
        }

        [TestMethod]
        public void Test_GetStocksList_Return4Items()
        {
            IEnumerable<Stock> actualStocks = StocksListParseHelper.GetStocksList(this.listContent);

            List<Stock> expectStocks = new List<Stock>
            {
                new Stock("浦发银行", "600000"),
                new Stock("邯郸钢铁", "600001"),
                new Stock("齐鲁退市", "600002"),
                new Stock("*ST石化A", "000013")
            };

            bool isEqual = !expectStocks.Where((t, i) => !actualStocks.ElementAt(i).Equals(t)).Any();
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void Test_GetStocksList_ReturnNotIncude031005()
        {
            IEnumerable<Stock> actualStocks = StocksListParseHelper.GetStocksList(this.listContent);
            Stock expectStock = new Stock("国安GAC1", "031005");

            bool isEqual = !actualStocks.Any(m => m.Equals(expectStock));
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void Test_GetStocksShortcut_First2Items() 
        {
            var shortcutList = StocksListParseHelper.GetStocksShortcut(stockContent);
            int i = 0;

            foreach (var shortcut in shortcutList) 
            {
                if (i >= 2)
                    break;
                ++i;

                if (shortcut.Key == "000001") 
                {
                    Assert.IsTrue(shortcut.Value == "PAYX");
                }
               

                if (shortcut.Key.Equals("000002"))
                {
                    Assert.IsTrue(shortcut.Value == "WKA");             
                }
            }
        }
        
    }
}
