﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Model.Test
{
    [TestClass]
    [DeploymentItem("stocksummary.htm")]
    public class SinaStockGeneralInfoParseTest
    {
        private readonly string _htmlContent;

        public SinaStockGeneralInfoParseTest()
        {
            const string testfile = "stockhistory_data.htm";
            StreamReader readStream = new StreamReader(testfile, System.Text.Encoding.GetEncoding("GBK"));
            _htmlContent = readStream.ReadToEnd();
        }

        [TestMethod]
        public void Test_GetStocksGerneralInfo_3Items()
        {
            IEnumerable<Stock> actualStocks = SinaStockGeneralInfoParse.GetStocksGerneralInfo(this._htmlContent);

            List<Stock> expectStocks = new List<Stock>
            {
                new Stock("浦发银行", "600000"),
                new Stock("邯郸钢铁", "600001"),
                new Stock("齐鲁退市", "600002"),
                new Stock("ST东北高","600003")
            };

            bool isEqual = !expectStocks.Where((t, i) => actualStocks.ElementAt(i) != t).Any();
            Assert.IsTrue(isEqual);
        }
    }
}
