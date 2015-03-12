using System;
using System.Collections.Generic;
using MongoDB.Driver;

namespace Infrastructure.Domain.MongoDb
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly MongoDatabase _database;
        private readonly Dictionary<Type, MongoCollection> _mongoCollections;
 
        public MongoDbContext()
        {
            this._mongoCollections = new Dictionary<Type, MongoCollection>();

            MongoServerSettings settings = new MongoServerSettings
            {
               Server = new MongoServerAddress("localhost")
            };

            MongoServer server = new MongoServer(settings);

            const string databaseName = "smartstock";

            this._database = server.GetDatabase(databaseName);
        }

        public MongoCollection GetMongoCollection(Type type)
        {
            if (this._mongoCollections.ContainsKey(type))
            {
                return this._mongoCollections[type];
            }

            MongoCollection mongoCollection = this._database.GetCollection(type.Name);
            this._mongoCollections.Add(type, mongoCollection);
            return mongoCollection;
        }
    }
}
