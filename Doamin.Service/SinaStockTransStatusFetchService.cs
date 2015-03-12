using System.Collections.Generic;
using System.Linq;
using Domain.Model;
using infrastructure.Utility;

namespace Domain.Service
{
    public class SinaStockTransStatusFetchService : WebRequestHandle, IStockTransStatusFetchService
    {
        private const string DailyLineUrl = @"http://vip.stock.finance.sina.com.cn/corp/go.php/vMS_MarketHistory/stockid/{0}.phtml?year={1}&jidu={2}";

        private const string SecondLineUrl = @"http://market.finance.sina.com.cn/transHis.php?symbol=sh{0}&date={1}&page={2}";

        private readonly string _stockCode;

        public SinaStockTransStatusFetchService(string code)
        {
            this._stockCode = code;
        }

        public IEnumerable<StockTransactionStatus> GetStocksByDateScope(DateScope dateScope)
        {
            IEnumerable<string> urls = this.GenerateStockUrls(dateScope);
            List<StockTransactionStatus> stockStatus = new List<StockTransactionStatus>();

            foreach (var url in urls)
            {
                IEnumerable<StockTransactionStatus> newStatuses = this.GetStocksByUrl(url);
                if(newStatuses == null)
                    continue;
                stockStatus.AddRange(newStatuses);
            }

            return stockStatus;
        }

        private IEnumerable<StockTransactionStatus> GetStocksByUrl(string url)
        {
            string htmlContent = base.GetHttpWebRequest(url);
            if (htmlContent == "")
                return null;
            return SinaStockTransStatusParse.GenerateStockStatus(htmlContent);
        } 

        private IEnumerable<string> GenerateStockUrls(DateScope date)
        {
            IEnumerable<KeyValuePair<int, Quarter>> quarters = date.GetQuarters();
            return quarters.Select(quarter => string.Format(DailyLineUrl, this._stockCode, quarter.Key, (int)quarter.Value)).ToList();
        }
    }
}
