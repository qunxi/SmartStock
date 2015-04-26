using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Stocks;

namespace Domain.Service.Crawl
{
    public interface IStockPersistService
    {
        void InitialAllStocksDailyHistory(DateTime startDate, DateTime endDate);
        void InitialStockList();
        void AddNewStocks(List<Stock> stocks);
        void UpdateStockDailyHistory(Stock stock, DateTime startDate, DateTime endDate);
        IEnumerable<Stock> GetStockList();
    }
}
