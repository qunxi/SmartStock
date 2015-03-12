using System.Collections.Generic;
using Domain.Model;
using infrastructure.Utility;

namespace Domain.Service
{
    public class SinaStockGeneralInfoFetchService : WebRequestHandle, IStockGeneralInfoFetchService
    {
        private const string Url = @"http://quote.eastmoney.com/stocklist.html#sh"; //不是来自新浪

        public IEnumerable<Stock> GeneralStocksGeneralInfo()
        {
            string htmlContent = GetHttpWebRequest(Url);
            return  SinaStockGeneralInfoParse.GetStocksGerneralInfo(htmlContent);
        }
    }
}
