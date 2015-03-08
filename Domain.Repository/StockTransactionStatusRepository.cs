using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;

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
