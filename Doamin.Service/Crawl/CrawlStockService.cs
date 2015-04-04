using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using Domain.Model;
using Domain.Model.Stocks;
using infrastructure.Utility;

namespace Domain.Service.Crawl
{
    public class CrawlStockService : WebRequestHandle, ICrawlStockService
    {
        public IEnumerable<Stock> GetAllStocksList()
        {
            const string shUrl = @"http://quote.eastmoney.com/stocklist.html#sh"; //东方财富
            const string szUrl = @"http://quote.eastmoney.com/stocklist.html#sz";

            List<Stock> stocks = new List<Stock>();

            string htmlContent = GetHttpWebRequest(shUrl);
            stocks.AddRange(StocksListParseHelper.GetStocksList(htmlContent));

            htmlContent = GetHttpWebRequest(szUrl);
            stocks.AddRange(StocksListParseHelper.GetStocksList(htmlContent));

            return stocks;
        }
        
        public Stock GetStockTransStatusByDate(Stock stock, DateScope dateScope)
        {
            IEnumerable<string> urls = this.GenerateStockUrls(stock.Code, dateScope);
            List<TransactionStatus> stockStatus = new List<TransactionStatus>();

            foreach (var url in urls)
            {
                IEnumerable<TransactionStatus> newStatuses = this.GetStocksByUrl(url);
                if (newStatuses == null)
                    continue;
                stockStatus.AddRange(newStatuses);
            }

            stock.DailyTransactionStatus = stockStatus;

            return stock;
        }

        #region private
        private IEnumerable<TransactionStatus> GetStocksByUrl(string url)
        {
            string htmlContent = base.GetHttpWebRequest(url);
            if (htmlContent == "")
                return null;
            return StockTransStatusParseHelper.GenerateStockStatus(htmlContent);
        }

        private IEnumerable<string> GenerateStockUrls(string code, DateScope date)
        {
            const string dailyLineUrl = @"http://vip.stock.finance.sina.com.cn/corp/go.php/vMS_MarketHistory/stockid/{0}.phtml?year={1}&jidu={2}";

            IEnumerable<KeyValuePair<int, Quarter>> quarters = date.GetQuarters();
            return quarters.Select(quarter => string.Format(dailyLineUrl, code, quarter.Key, (int)quarter.Value)).ToList();
        }
        #endregion private
    }
}
