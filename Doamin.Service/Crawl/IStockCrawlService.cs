using System.Collections.Generic;
using Domain.Model;
using Domain.Model.Stocks;

namespace Domain.Service.Crawl
{
    public interface IStockCrawlService
    {
        IEnumerable<Stock> GetAllStocksList();
        Stock GetStockTransStatusByDate(Stock stock, DateScope dateScope);
        Stock GetStockTransStatusByUrls(Stock stock, IEnumerable<string> urls);
    }
}
