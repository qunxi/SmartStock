using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Domain.Model;

namespace Domain
{
    public class SinaStockTransStatusParse
    {
        private const string pattern = @"<div align=""center"">\W*(?<value>[\d\.-]+?)\W*</div>|<a[^>]*href=[""|'](?<link>.*)['|""]>\W*(?<value>[\d-]+?)\W*</a>";
           
        public static IEnumerable<StockTransactionStatus> GenerateStockStatus(string htmlContent)
        {
            string historyContent = GetHistoryTable(htmlContent);
            KeyValuePair<string, string> codeName = GetStockCodeName(historyContent);
            var matches = Regex.Matches(historyContent, pattern);
            List<StockTransactionStatus> stockStatus = new List<StockTransactionStatus>();
            for (int i = 0; i < matches.Count; i++)
            {
                StockTransactionStatus transStatus = new StockTransactionStatus
                    {
                        Name = codeName.Value,
                        Code = codeName.Key,
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

        public static Stock GenerateStock(string htmlContent)
        {
            string historyContent = GetHistoryTable(htmlContent);
            KeyValuePair<string, string> codeName = GetStockCodeName(historyContent);

            var matches = Regex.Matches(historyContent, pattern);
            Stock stock = new Stock(codeName.Value, codeName.Key);
            
            for (int i = 0; i < matches.Count; i++)
            {
                stock.AddTransactionStatus(
                    new StockTransactionStatus
                    {
                        Date = DateTime.ParseExact(matches[i].Groups["value"].Value.Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture),
                        Open = Convert.ToDouble(matches[++i].Groups["value"].Value),
                        High = Convert.ToDouble(matches[++i].Groups["value"].Value),
                        Close = Convert.ToDouble(matches[++i].Groups["value"].Value),
                        RealTime = Convert.ToDouble(matches[i].Groups["value"].Value), // same as close
                        Low = Convert.ToDouble(matches[++i].Groups["value"].Value),
                        Volume = Convert.ToDouble(matches[++i].Groups["value"].Value),
                        Turnover = Convert.ToDouble(matches[++i].Groups["value"].Value)
                    });
            }

            return stock;
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
