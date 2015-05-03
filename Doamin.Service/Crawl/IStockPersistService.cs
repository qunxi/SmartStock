using System;
using System.Collections.Generic;
using Domain.Model.Stocks;

namespace Domain.Service.Crawl
{
    public interface IStockPersistService
    {
        void InitialAllStocksDailyHistory(DateTime startDate, DateTime endDate);
        void InitialStockList();
        void AddNewStocks(List<Stock> stocks);
        void UpdateStockDailyHistoryByUrls(Stock stock, IEnumerable<string> urls);
        void UpdateStockDailyHistoryByDate(Stock stock, DateTime startDate, DateTime endDate);
        IEnumerable<Stock> GetStockList();
    }
}
