using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Model;
using Domain.Model.Stocks;
using Domain.Repository;
using Infrastructure.Domain;
using Infrastructure.Utility.Logging;

namespace Domain.Service.Crawl
{
    public class StockPersistService : IStockPersistService
    {
        private readonly IRepository<Stock> stockRepository;
        private readonly ITransactionStatusRepository transRepository;
        private readonly IStockCrawlService crawlService;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger logger;
       
        public StockPersistService(IRepository<Stock> stockRepository,
                                   ITransactionStatusRepository transRepository,
                                   IUnitOfWork unitOfWork,
                                   ILogger logger,
                                   IStockCrawlService crawlService)
        {
            this.transRepository = transRepository;
            this.stockRepository = stockRepository;
            this.crawlService = crawlService;
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        public void InitialStockList()
        {
            IEnumerable<Stock> stocks = this.crawlService.GetAllStocksList();

            foreach (var stock in stocks)
            {
                if (!this.stockRepository.Exist(stock))
                {
                    this.stockRepository.Add(stock); 
                }
            }
            this.unitOfWork.Commit();
        }

        public void AddNewStocks(List<Stock> stocks)
        {
            foreach (var stock in stocks)
            {
                this.stockRepository.Add(stock);
                this.unitOfWork.Commit();
            }
        }

        public void InitialAllStocksDailyHistory(DateTime startTime, DateTime endTime)
        {
            // why convert IEnumerable to List? it take long time to cawl stock data from internet,
            // so it will cause mongodb cursor time out. 
            List<Stock> stocks = this.stockRepository.FindAll().ToList();
            foreach (var stock in stocks)
            {
                this.UpdateStockDailyHistoryByDate(stock, startTime, endTime);
            }
        }

        public void UpdateStockDailyHistoryByUrls(Stock stock, IEnumerable<string> urls)
        {
            Stock newStock = this.crawlService.GetStockTransStatusByUrls(stock, urls);
            this.AddStockDailyHistory(newStock); 
        }

        public void UpdateStockDailyHistoryByDate(Stock stock, DateTime startTime, DateTime endTime)
        {
            try
            {
                DateTime latestTime = this.transRepository.GetLatestUpdatedInDb(stock, startTime);

                DateScope dateSoScope = new DateScope(latestTime, endTime);
                Stock newStock = this.crawlService.GetStockTransStatusByDate(stock, dateSoScope);
                this.AddStockDailyHistory(newStock);
            }
            catch (Exception e)
            {
                System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(1, true);
                int line = st.GetFrame(0).GetFileLineNumber();
                string file = st.GetFrame(0).GetFileName();
                logger.Error(string.Format("the source file {0} at line {1} throw execption: {2}. the stock is {3}",
                    file, line, e.Message, stock.Code + stock.Name));
            }
        }

        public void UpdateStockShortcutName() 
        {
            var shortcutList = this.crawlService.GetStockShortcutList();
            var stockList = GetStockList();
            foreach (var stock in stockList) 
            {
                if (shortcutList.Keys.Any(k => k == stock.Code))
                {
                    stock.ShortcutName = shortcutList[stock.Code];
                }
                else 
                {
                    logger.Error(string.Format("the stock {0} didn't find shortcut name", stock.Code));
                }

                this.stockRepository.Update(stock);
            }
            this.unitOfWork.Commit();
        }

        public IEnumerable<Stock> GetStockList()
        {
            return this.stockRepository.FindAll();
        }

        private void AddStockDailyHistory(Stock stock)
        {
            this.transRepository.AddStockTransactionStatus(stock);
            this.unitOfWork.Commit();
        }
    }
}
