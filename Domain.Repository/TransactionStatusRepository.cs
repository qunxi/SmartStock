using Domain.Model.Stocks;
using Infrastructure.Domain;
using Infrastructure.Domain.MongoDb;
using MongoDB.Driver;

namespace Domain.Repository
{
    public class TransactionStatusRepository : MongoDbRepository<TransactionStatus>, ITransactionStatusRepository
    {
        public TransactionStatusRepository(IMongoDbContext dbContext, IUnitOfWork unitOfWork) 
            : base(dbContext, unitOfWork)
        {
        }

        protected override MongoCollection GetMongoCollection()
        {
            return this._dbContext.GetMongoCollection(CollectionName);
        }

        public string CollectionName { get; set; }

        public void AddTransactionStatus(TransactionStatus transStatus)
        {
            this.Add(transStatus);
        }
    }
}
