using System;
using System.Collections.Generic;
using Domain;
using Domain.Model;
using Domain.Repository;

namespace Application.Service
{
    public class StoreSinaTransactionService
    {
        private readonly IStockTransactionStatusRepository _stockStatusRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StoreSinaTransactionService(IStockTransactionStatusRepository stockStatusRepository,
                                           IStockRepository stockRepository,
                                           IUnitOfWork unitOfWork)
        {
            this._stockStatusRepository = stockStatusRepository;
            this._stockRepository = stockRepository;
            this._unitOfWork = unitOfWork;
        }


        public void SaveStocksGeneralInfoIntoDatabase(IStockGeneralInfoFetchService service)
        {
            IEnumerable<Stock> stocks = service.GeneralStocksGeneralInfo();

            foreach (Stock stock in stocks)
            {
                this._stockRepository.SaveStock(stock);
            }

            this._unitOfWork.Commit();
        }

        public void SaveStocksStatusIntoDatabase(IStockGeneralInfoFetchService service)
        {
            IEnumerable<Stock> stocks = service.GeneralStocksGeneralInfo();
            foreach (var stock in stocks)
            {
                SaveStocksIntoDatabase(stock.Code, new DateScope(new DateTime(1990, 1, 1), new DateTime(2015, 3, 31)));
            }
        }

        public void SaveStocksIntoDatabase(string code, DateScope dateScope) 
        {
            IStockTransStatusFetchService fetchService = new SinaStockTransStatusFetchService(code);

            IEnumerable<StockTransactionStatus> stockStatus = fetchService.GetStocksByDateScope(dateScope);
            
            foreach(StockTransactionStatus status in stockStatus)
            {
                this._stockStatusRepository.SaveStockStatus(status);
            }

            this._unitOfWork.Commit();
        }
      
    }
}
