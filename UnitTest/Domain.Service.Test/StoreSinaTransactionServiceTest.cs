using Domain.Repository;
using Domain.Service;
using Infrastructure.Domain;
using Infrastructure.Domain.MongoDb;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreSinaTransactionService = Domain.Service.StoreSinaTransactionService;

namespace Application.Test
{
    [TestClass]
    public class StoreSinaTransactionServiceTest
    {
        [TestMethod]
        public void Test_SaveStocksStatusIntoDatabase_Success()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
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
            IUnitOfWork unitOfWork = new UnitOfWork();
            IMongoDbContext mongoDbContext = new MongoDbContext();
            IStockTransactionStatusRepository stockStatusRepository = new StockTransactionStatusRepository(mongoDbContext, unitOfWork);
            IStockRepository stockRepository = new StockRepository(mongoDbContext, unitOfWork);
            StoreSinaTransactionService service = new StoreSinaTransactionService(stockStatusRepository, stockRepository, unitOfWork);
            IStockGeneralInfoFetchService infoService = new SinaStockGeneralInfoFetchService();
            service.SaveStocksGeneralInfoIntoDatabase(infoService);
        }
    }
}
