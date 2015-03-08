using System;
using MongoDB.Driver;

namespace Domain.Repository
{
    public interface IMongoDbContext
    {
        MongoCollection GetMongoCollection(Type type);
    }
}
