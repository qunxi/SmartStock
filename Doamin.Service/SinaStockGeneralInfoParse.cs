using System.Collections.Generic;
using System.Text.RegularExpressions;
using Domain.Model;

namespace Domain.Service
{
    public static class SinaStockGeneralInfoParse
    {
        private const string Pattern = @"<li><a[^>]*>(?<name>\w*?)\((?<code>[6|3|0]\d+?)\)</a></li>";

        public static IEnumerable<Stock> GetStocksGerneralInfo(string htmlContent)
        {
            var matches = Regex.Matches(htmlContent, Pattern);
            List<Stock> stocks = new List<Stock>();

            for (int i = 0; i < matches.Count; i++)
            {
                Stock stock = new Stock(matches[i].Groups["name"].Value, matches[i].Groups["code"].Value);
                stocks.Add(stock);
            }

            return stocks;
        }
    }
}
