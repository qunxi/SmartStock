using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Domain.Model.Stocks;
using Domain.Service.Crawl;
using Infrastructure.Domain;
using Infrastructure.Utility.Logging;

namespace Domain.Service.Maintain
{
    public class MaintainService : IMaintainService
    {
        private readonly IStockPersistService stockPersistService;
        private readonly IRepository<Stock> stockRepository;
        private readonly ILogger logger;

        public MaintainService(IStockPersistService stockService,  IRepository<Stock> stockRepository, ILogger logger)
        {
            this.stockPersistService = stockService;
            this.stockRepository = stockRepository;
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
                this.stockPersistService.UpdateStockDailyHistoryByDate(stock, start, end);
            }
        }

        public void RemoveStockHistory(string stockCode)
        {
            throw new NotImplementedException();
        }

        public void UpdateLoggingFileData()
        {
            StreamReader streamRead = new StreamReader(@"C:\Users\cnwanaar\Desktop\github\SmartStock\SmartStock.Web\logs\logging.txt");
            string line;
            while ((line = streamRead.ReadLine()) != null)
            {
                const string pattern = @"(http[^\d]+(\d+).phtml[\w\W]+) failed";

                var matche = Regex.Match(line, pattern);
                string address = matche.Groups[1].Value;
                string code = matche.Groups[2].Value;

                Stock stock = this.stockRepository.Find(m => m.Code == code);
                if (stock == null) 
                {
                    this.logger.Error(string.Format("The stock is not exist {0}", code));
                    continue;
                }

                this.stockPersistService.UpdateStockDailyHistoryByUrls(stock, new List<string>{address});
            }
            
        }
    }
}
