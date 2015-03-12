using System.Collections.Generic;
using Domain.Model;
using Infrastructure.Domain;
using Infrastructure.Domain.MongoDb;

namespace Domain.Repository
{
    public class StockRepository : MongoDbRepository<Stock>, IStockRepository
    {
        public StockRepository(IMongoDbContext dbContext, IUnitOfWork unitOfWork) 
            : base(dbContext, unitOfWork)
        {
        }

        public void StoreStockInfoToDatabase(IEnumerable<Stock> stocks)
        {
            foreach (var stock in stocks)
            {
                RegisterAdd(stock);
            }
        }

        public void SaveStock(Stock stock)
        {
            RegisterAdd(stock);
        }

        public void UpdateStock(Stock stock)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveStock(Stock stock)
        {
            throw new System.NotImplementedException();
        }
    }
}
