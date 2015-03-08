using Application.Service;
using Domain;
using Domain.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application.Test
{
    [TestClass]
    public class StoreSinaTransactionServiceTest
    {
        [TestMethod]
        public void Test_SaveStocksStatusIntoDatabase_Success()
        {
            IUnitOfWork unitOfWork = new MongoDbUnitOfWork();
            IMongoDbContext mongoDbContext = new MongoDbContext();
            IStockTransactionStatusRepository stockStatusRepository = new StockTransactionStatusRepository(mongoDbContext, unitOfWork);
            IStockRepository stockRepository = new StockRepository(mongoDbContext, unitOfWork);
            StoreSinaTransactionService service = new StoreSinaTransactionService(stockStatusRepository, stockRepository, unitOfWork);
            IStockGeneralInfoFetchService infoService = new SinaStockGeneralInfoFetchService();
            service.SaveStocksStatusIntoDatabase(infoService);
        }

        [TestMethod]
        public void Test_SaveStocksGeneralInfoIntoDatabase_Success()
        {
            IUnitOfWork unitOfWork = new MongoDbUnitOfWork();
            IMongoDbContext mongoDbContext = new MongoDbContext();
            IStockTransactionStatusRepository stockStatusRepository = new StockTransactionStatusRepository(mongoDbContext, unitOfWork);
            IStockRepository stockRepository = new StockRepository(mongoDbContext, unitOfWork);
            StoreSinaTransactionService service = new StoreSinaTransactionService(stockStatusRepository, stockRepository, unitOfWork);
            IStockGeneralInfoFetchService infoService = new SinaStockGeneralInfoFetchService();
            service.SaveStocksGeneralInfoIntoDatabase(infoService);
        }
    }
}
