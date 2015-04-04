using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Domain.Model.Stocks;

namespace Domain.Service.Crawl
{
    public static class StockTransStatusParseHelper
    {
        /*public static Stock GenerateStock(string htmlContent)
        {
            string historyContent = GetHistoryTable(htmlContent);
            KeyValuePair<string, string> codeName = GetStockCodeName(historyContent);
            string stockName = codeName.Value;
            string stockCode = codeName.Key;

            Stock stock = new Stock(stockName, stockCode);
            stock.DailyTransactionStatus = GenerateStockStatus(stockName, stockCode, historyContent);

            return stock;
        }*/

        public static IEnumerable<TransactionStatus> GenerateStockStatus(string htmlContent)
        {
            const string statusPattern = @"<div align=""center"">\W*(?<value>[\d\.-]+?)\W*</div>|<a[^>]*href=[""|'](?<link>.*)['|""]>\W*(?<value>[\d-]+?)\W*</a>";

            string historyContent = GetHistoryTable(htmlContent);
            KeyValuePair<string, string> codeName = GetStockCodeName(historyContent);
            string stockName = codeName.Value;
            string stockCode = codeName.Key;

            var matches = Regex.Matches(historyContent, statusPattern);
            List<TransactionStatus> stockStatus = new List<TransactionStatus>();
            for (int i = 0; i < matches.Count; i++)
            {
                TransactionStatus transStatus = new TransactionStatus
                {
                    Name = stockName,
                    Code = stockCode,
                    Date = DateTime.ParseExact(matches[i].Groups["value"].Value.Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    Open = Convert.ToDouble(matches[++i].Groups["value"].Value),
                    High = Convert.ToDouble(matches[++i].Groups["value"].Value),
                    Close = Convert.ToDouble(matches[++i].Groups["value"].Value),
                    RealTime = Convert.ToDouble(matches[i].Groups["value"].Value), // same as close
                    Low = Convert.ToDouble(matches[++i].Groups["value"].Value),
                    Volume = Convert.ToDouble(matches[++i].Groups["value"].Value),
                    Turnover = Convert.ToDouble(matches[++i].Groups["value"].Value)
                };

                stockStatus.Add(transStatus);
            }

            return stockStatus;
        }

        private static string GetHistoryTable(string htmlContent)
        {
            return Regex.Match(htmlContent, @"<table.*FundHoldSharesTable[^>]*>[\w\W]+</table>").Value;
        }

        private static KeyValuePair<string, string> GetStockCodeName(string historyContent)
        {
            Match match = Regex.Match(historyContent, @"<th [^>]+>(?<name>[\w\W]+)\((?<code>\d+)\)");

            return match.Groups.Count == 3 ? new KeyValuePair<string, string>(match.Groups["code"].Value, match.Groups["name"].Value.Trim()) 
                                           : new KeyValuePair<string, string>();
        }
    }
}
