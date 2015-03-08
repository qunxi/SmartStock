using System;
using Application.Service;
using Domain;
using Domain.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            IUnitOfWork unitOfWork = new MongoDbUnitOfWork();
            IMongoDbContext mongoDbContext = new MongoDbContext();
            IStockTransactionStatusRepository stockStatusRepository = new StockTransactionStatusRepository(mongoDbContext, unitOfWork);
            IStockRepository stockRepository = new StockRepository(mongoDbContext, unitOfWork);
            StoreSinaTransactionService service = new StoreSinaTransactionService(stockStatusRepository, stockRepository, unitOfWork);
            IStockGeneralInfoFetchService infoService = new SinaStockGeneralInfoFetchService();
            service.SaveStocksStatusIntoDatabase(infoService);
            // service.SaveStocksIntoDatabase("600030", new DateScope(new DateTime(2015, 1, 1), new DateTime(2015, 3, 7) ));
        }

        [TestMethod]
        public void TestMethod2()
        {
            IUnitOfWork unitOfWork = new MongoDbUnitOfWork();
            IMongoDbContext mongoDbContext = new MongoDbContext();
            IStockTransactionStatusRepository stockStatusRepository = new StockTransactionStatusRepository(mongoDbContext, unitOfWork);
            IStockRepository stockRepository = new StockRepository(mongoDbContext, unitOfWork);
            StoreSinaTransactionService service = new StoreSinaTransactionService(stockStatusRepository, stockRepository, unitOfWork);
            IStockGeneralInfoFetchService infoService = new SinaStockGeneralInfoFetchService();
            service.SaveStocksGeneralInfoIntoDatabase(infoService);

            // service.SaveStocksIntoDatabase("600030", new DateScope(new DateTime(2015, 1, 1), new DateTime(2015, 3, 7)));
        }
    }
}
