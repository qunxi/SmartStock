using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Model;
using Domain.Model.Stocks;
using Domain.Repository;
using Infrastructure.Domain;

namespace Domain.Service.Crawl
{
    public class StockPersistService : IStockPersistService
    {
        private readonly IRepository<Stock> stockRepository;
        private readonly ITransactionStatusRepository transRepository;
        private readonly IStockCrawlService crawlService;
        private readonly IUnitOfWork unitOfWork;
       
        public StockPersistService(IRepository<Stock> stockRepository,
                                   ITransactionStatusRepository transRepository,
                                   IUnitOfWork unitOfWork,
                                   IStockCrawlService crawlService)
        {
            this.transRepository = transRepository;
            this.stockRepository = stockRepository;
            this.crawlService = crawlService;
            this.unitOfWork = unitOfWork;
        }

        public void InitialStockList()
        {
            IEnumerable<Stock> stocks = this.crawlService.GetAllStocksList();
            foreach (var stock in stocks)
            {
               this.stockRepository.Add(stock); 
            }
            this.unitOfWork.Commit();
        }

        public void AddNewStocks(List<Stock> stocks)
        {
            foreach (var stock in stocks)
            {
                this.stockRepository.Add(stock);
                unitOfWork.Commit();
                this.AddStockDailyHistory(stock);
            }
        }

        public void InitialAllStocksDailyHistory(DateTime startTime, DateTime endTime)
        {
            List<Stock> stocks = this.stockRepository.FindAll().ToList();
            foreach (var stock in stocks)
            {
                this.UpdateStockDailyHistory(stock, startTime, endTime);
            }
        }

        public void UpdateStockDailyHistory(Stock stock, DateTime startTime, DateTime endTime)
        {
            DateScope dateSoScope = new DateScope(startTime, endTime);
            Stock newStock = this.crawlService.GetStockTransStatusByDate(stock, dateSoScope);
            this.AddStockDailyHistory(newStock);
        }

        public IEnumerable<Stock> GetStockList()
        {
            return this.stockRepository.FindAll();
        }

        private void AddStockDailyHistory(Stock stock)
        {
            this.transRepository.CollectionName =  stock.Code + stock.Name; //(60000浦发银行)
            foreach (var transStatus in stock.DailyTransactionStatus)
            {
                this.transRepository.Add(transStatus);
            }
            this.unitOfWork.Commit();
        }
    }
}
