using System.Linq;
using Domain.Model.Stocks;
using Infrastructure.Domain;
using Infrastructure.Domain.MongoDb;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Domain.Repository
{
    public class StockRepository : MongoDbRepository<Stock> 
    {
        public StockRepository(IMongoDbContext dbContext, IUnitOfWork unitOfWork) 
            : base(dbContext, unitOfWork)
        {
        }

        public override bool Exist (Stock stock)
        {
            MongoCollection collection = GetMongoCollection();
            return collection.AsQueryable<Stock>().Any(doc => doc.Code == stock.Code);
        }
    }
}
