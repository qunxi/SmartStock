using System;
using System.Collections.Generic;
using Domain.Model;
using Domain.Model.Stocks;
using Domain.Repository;
using Infrastructure.Domain;

namespace Domain.Service.Crawl
{
    public class StoreStocksService
    {
        private readonly IRepository<Stock> _stockRepository;
        private readonly ITransactionStatusRepository _transRepository;
        private readonly ICrawlStockService _crawlService;
        private readonly IUnitOfWork _unitOfWork;

        public StoreStocksService(IRepository<Stock> stockRepository,
                                  ITransactionStatusRepository transRepository,
                                  ICrawlStockService crawlService,
                                  IUnitOfWork unitOfWork)
        {
            this._transRepository = transRepository;
            this._stockRepository = stockRepository;
            this._crawlService = crawlService;
            this._unitOfWork = unitOfWork;
        }

        public void SaveStocksListToDatabase()
        {
            IEnumerable<Stock> stocks = this._crawlService.GetAllStocksList();
            foreach (var stock in stocks)
            {
               this._stockRepository.Add(stock); 
            }
            this._unitOfWork.Commit();
        }

        public void SaveTransactionStatusToDatabase(DateTime startTime)
        {
            IEnumerable<Stock> stocks = this._stockRepository.FindAll();
            foreach (var stock in stocks)
            {
                DateScope dateSoScope = new DateScope(startTime, DateTime.Now);
                Stock newStock = this._crawlService.GetStockTransStatusByDate(stock, dateSoScope);
                this._transRepository.CollectionName = newStock.Name + newStock.Code; //(浦发银行60000)
                foreach (var transStatus in stock.DailyTransactionStatus)
                {
                    this._transRepository.AddTransactionStatus(transStatus);
                }
                this._unitOfWork.Commit();
            }
        }
    }
}
