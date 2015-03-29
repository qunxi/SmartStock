using Domain.Repository;
using Domain.Service;
using Infrastructure.Domain;
using Infrastructure.Domain.MongoDb;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            ITransactionStatusRepository stockStatusRepository = new TransactionStatusRepository(mongoDbContext,
                unitOfWork);
        }

        [TestMethod]
        public void Test_SaveStocksGeneralInfoIntoDatabase_Success()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            IMongoDbContext mongoDbContext = new MongoDbContext();
            ITransactionStatusRepository stockStatusRepository = new TransactionStatusRepository(mongoDbContext, unitOfWork);
        }
    }
}
