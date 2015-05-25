using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Domain.Model;
using Domain.Model.Stocks;
using infrastructure.Utility;
using Infrastructure.Utility.Logging;

namespace Domain.Service.Crawl
{
    public class StockCrawlService : WebRequestHandle, IStockCrawlService
    {
        private readonly ILogger logger;
        
        public StockCrawlService(ILogger logger)
            : base(logger)
        {
            this.logger = logger;
        }

        public IEnumerable<Stock> GetAllStocksList()
        {
            const string stockListUrl = @"http://quote.eastmoney.com/stocklist.html"; //东方财富

            List<Stock> stocks = new List<Stock>();

            string htmlContent = GetHttpWebRequest(stockListUrl, Encoding.GetEncoding("GBK"));
            
            stocks.AddRange(StocksListParseHelper.GetStocksList(htmlContent));

            return stocks;
        }

        public Stock GetStockTransStatusByUrls(Stock stock, IEnumerable<string> urls)
        {
            List<TransactionStatus> stockStatus = new List<TransactionStatus>();

            foreach (var url in urls)
            {
                IEnumerable<TransactionStatus> newStatuses = this.GetTransactionStatusByUrl(url);

                if (newStatuses == null)
                {
                    continue;
                }

                stockStatus.AddRange(newStatuses);
            }

            stock.DailyTransactionStatus = stockStatus;

            return stock;
        }

        public Stock GetStockTransStatusByDate(Stock stock, DateScope dateScope)
        {
            IEnumerable<string> urls = this.GenerateStockUrls(stock.Code, dateScope);
  
            stock.DailyTransactionStatus =  GetStockTransStatusByUrls(stock, urls).DailyTransactionStatus.Where(m => m.Date >= dateScope.Start);

            return stock;
        }

        #region private
        private IEnumerable<TransactionStatus> GetTransactionStatusByUrl(string url)
        {
            try
            {
                string htmlContent = this.GetHttpWebRequestNoWebException(url, Encoding.GetEncoding("GBK"));

                return string.IsNullOrEmpty(htmlContent) ? null : StockTransStatusParseHelper.GenerateStockStatus(htmlContent);
            }
            catch (WebException e)
            {
                // I don't know how to fixed the httpWebRequest throw Timeout WebException, it always happen.
                // Do I need use loop to request nutil not happend timeout
                this.logger.Error(string.Format("Access the address {0} failed. Exception is {1}", url, e.Message));
            }

            return null;
        }

        private IEnumerable<string> GenerateStockUrls(string code, DateScope date)
        {
            const string dailyLineUrl = @"http://vip.stock.finance.sina.com.cn/corp/go.php/vMS_MarketHistory/stockid/{0}.phtml?year={1}&jidu={2}";

            IEnumerable<KeyValuePair<int, Quarter>> quarters = date.GetQuarters();
            return quarters.Select(quarter => string.Format(dailyLineUrl, code, quarter.Key, (int)quarter.Value)).ToList();
        }


        //later i need add update short cut name function for stock, why not invoke the initial stock function?
        //it takes long time to crawl web, so as an indiviual function to do things.
        public Dictionary<string, string> GetStockShortcutList()
        {
            const string baseUrl = @"http://www.yz21.org/stock/info";

            Dictionary<string, string> shortCutsList = new Dictionary<string, string>();

            const int TotalPage = 139; //current the yz21.org just has 139 pages

            for (int i = 1; i <= TotalPage; i++)
            {
                string url = i == 1 ? baseUrl : string.Format(baseUrl + "/stocklist_{0}.html", i);
                try
                {
                    string htmlContent = this.GetHttpWebRequestNoWebException(url, Encoding.UTF8);

                    if(string.IsNullOrEmpty(htmlContent))
                        continue;
                    
                    shortCutsList = shortCutsList.Union(StocksListParseHelper.GetStocksShortcut(htmlContent))
                        .ToDictionary(key => key.Key, value => value.Value);
                }
                catch (WebException e)
                {
                    // I don't know how to fixed the httpWebRequest throw Timeout WebException, it always happen.
                    // Do I need use loop to request nutil not happend timeout
                    this.logger.Error(string.Format("Access the address {0} failed. Exception is {1}", url, e.Message));
                } 
            }
            return shortCutsList;
        }

        #endregion private
    }
}
