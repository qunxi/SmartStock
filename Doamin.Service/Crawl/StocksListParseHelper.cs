using System.Collections.Generic;
using System.Text.RegularExpressions;
using Domain.Model.Stocks;

namespace Domain.Service.Crawl
{
    public static class StocksListParseHelper
    {
        public static IEnumerable<Stock> GetStocksList(string htmlContent)
        {
            const string pattern = @"<li><a[^>]*>(?<name>[\w\*]*?)\((?<code>[6|3|0]0\d+?)\)</a></li>";

            var matches = Regex.Matches(htmlContent, pattern);
            List<Stock> stocks = new List<Stock>();

            for (int i = 0; i < matches.Count; i++)
            {
                Stock stock = new Stock(matches[i].Groups["name"].Value, matches[i].Groups["code"].Value);
                stocks.Add(stock);
            }

            return stocks;
        }

        /*public static Stock GetStockGeneralInfo(Stock stock, string htmlContent)
        {
            
        }*/

        public static Dictionary<string, string> GetStocksShortcut(string htmlContent)
        {
           const string pattern =
                @"<td><a[^>]+?>(?<code>\d+?)</a></td><td><a[^>]+?>(?<name>[\w\W]+?)</a></td><td><a[^>]+?>(?<full>[\w\W]+?)</a></td><td>(?<short>[A-Za-z]+?)</td>\W*</tr>";
            var matches = Regex.Matches(htmlContent, pattern);
            
            Dictionary<string, string> shortCuts = new Dictionary<string, string>();

            for (int i = 0; i < matches.Count; i++)
            {
                KeyValuePair<string, string> shortCut = new KeyValuePair<string, string>(matches[i].Groups["code"].Value, matches[i].Groups["short"].Value);
                shortCuts.Add(shortCut.Key, shortCut.Value);
            }

            return shortCuts;
        }
    }
}
