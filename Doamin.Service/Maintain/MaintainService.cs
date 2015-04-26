using System;
using Domain.Service.Crawl;
using Infrastructure.Utility.Logging;

namespace Domain.Service.Maintain
{
    public class MaintainService : IMaintainService
    {
        private readonly IStockPersistService stockPersistService;
        private ILogger logger;

        public MaintainService(IStockPersistService stockService, ILogger logger)
        {
            this.stockPersistService = stockService;
            this.logger = logger;
        }

        public void InitialAllStocksHistory()
        {
            this.stockPersistService.InitialStockList();
            //1990-12-19 shanghai stock exchange opening ceremony time
            DateTime startDate = new DateTime(1990, 12, 19);
            this.stockPersistService.InitialAllStocksDailyHistory(startDate, DateTime.Now);
        }

        public void UpdateAllStocksHistory(DateTime start, DateTime end)
        {
            var stocks = this.stockPersistService.GetStockList();
            foreach (var stock in stocks)
            {
                this.stockPersistService.UpdateStockDailyHistory(stock, start, end);
            }
        }

        public void RemoveStockHistory(string stockCode)
        {
            throw new NotImplementedException();
        }
    }
}
