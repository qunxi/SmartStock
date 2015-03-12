using Domain.Model;
using Infrastructure.Domain;
using Infrastructure.Domain.MongoDb;

namespace Domain.Repository
{
    public class StockTransactionStatusRepository : MongoDbRepository<StockTransactionStatus>, IStockTransactionStatusRepository
    {
        public StockTransactionStatusRepository(IMongoDbContext dbContext, IUnitOfWork unitOfWork) 
            : base(dbContext, unitOfWork)
        {
        }

        public void SaveStockStatus(StockTransactionStatus status)
        {
            RegisterAdd(status);
        }
    }
}
