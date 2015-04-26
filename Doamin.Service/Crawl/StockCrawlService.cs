using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using Domain.Model;
using Domain.Model.Stocks;
using infrastructure.Utility;
using Infrastructure.Utility.Logging;

namespace Domain.Service.Crawl
{
    public class StockCrawlService : WebRequestHandle, IStockCrawlService
    {
        private ILogger logger;
        
        public StockCrawlService(ILogger logger)
        {
            this.logger = logger;
        }

        public IEnumerable<Stock> GetAllStocksList()
        {
            const string stockListUrl = @"http://quote.eastmoney.com/stocklist.html"; //东方财富

            List<Stock> stocks = new List<Stock>();

            string htmlContent = GetHttpWebRequest(stockListUrl);
            stocks.AddRange(StocksListParseHelper.GetStocksList(htmlContent));

            return stocks;
        }
        
        public Stock GetStockTransStatusByDate(Stock stock, DateScope dateScope)
        {
            IEnumerable<string> urls = this.GenerateStockUrls(stock.Code, dateScope);
            List<TransactionStatus> stockStatus = new List<TransactionStatus>();

            foreach (var url in urls)
            {
                IEnumerable<TransactionStatus> newStatuses = this.GetTransactionStatusByUrl(url);
                if (newStatuses == null)
                    continue;
                stockStatus.AddRange(newStatuses);
            }

            stock.DailyTransactionStatus = stockStatus;

            return stock;
        }

        #region private
        private IEnumerable<TransactionStatus> GetTransactionStatusByUrl(string url)
        {
            try
            {
                string htmlContent = base.GetHttpWebRequest(url);

                if (string.IsNullOrEmpty(htmlContent))
                {
                    return null;
                }
                return StockTransStatusParseHelper.GenerateStockStatus(htmlContent);
            }
            catch (WebException e)
            {
                System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(1, true);
                int line = st.GetFrame(0).GetFileLineNumber();
                string file = st.GetFrame(0).GetFileName();
                this.logger.Error(string.Format("at Line: {0} of File: {1} throw an exeception: {2}. the url is {3}", line, file, e.Message, url));
            }

            return null;
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
