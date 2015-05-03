using System;
using System.Linq;
using Domain.Model.Stocks;
using Infrastructure.Domain;
using Infrastructure.Domain.MongoDb;
using MongoDB.Driver;

namespace Domain.Repository
{
    //note get the date from mongodb need to convert localtime because the mongodb store with UTC
    public class TransactionStatusRepository : MongoDbRepository<TransactionStatus>, ITransactionStatusRepository
    {
        private string collectionName;

        public TransactionStatusRepository(IMongoDbContext dbContext, IUnitOfWork unitOfWork) 
            : base(dbContext, unitOfWork)
        {
        }

        // override the function of MongoDbRepository
        protected override MongoCollection GetMongoCollection()
        {
            return this._dbContext.GetMongoCollection(this.collectionName);
        }

        public string CollectionName
        {
            get { return this.collectionName; } 
        }

        public void AddStockTransactionStatus(Stock stock)
        {
            this.collectionName = stock.Code + stock.Name;

            foreach (var transStatus in stock.DailyTransactionStatus)
            {
                base.Add(transStatus);
            }
        }

        public DateTime GetLatestUpdatedInDb(Stock stock, DateTime startDate)
        {
            this.collectionName = stock.Code + stock.Name;
            var status = base.FindAll(m => true, m => m.Date, SortOrder.Descending).FirstOrDefault();

            if (status != null)
            {
                return status.Date.AddDays(1).ToLocalTime(); //mongodb store time as UTC, so it should convert to local time.
            }

            return startDate;
         }
    }
}
