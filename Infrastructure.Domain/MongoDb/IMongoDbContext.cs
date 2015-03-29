using System;
using MongoDB.Driver;

namespace Infrastructure.Domain.MongoDb
{
    public interface IMongoDbContext
    {
        MongoCollection GetMongoCollection(Type type);
        MongoCollection GetMongoCollection(string collectionName);
    }
}
