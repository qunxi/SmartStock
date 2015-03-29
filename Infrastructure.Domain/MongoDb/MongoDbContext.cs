using System;
using System.Collections.Generic;
using MongoDB.Driver;

namespace Infrastructure.Domain.MongoDb
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly MongoDatabase _database;
        private readonly Dictionary<string, MongoCollection> _mongoCollections; 
 
        public MongoDbContext()
        {
            this._mongoCollections = new Dictionary<string, MongoCollection>();

            MongoServerSettings settings = new MongoServerSettings
            {
               Server = new MongoServerAddress("localhost")
            };

            MongoServer server = new MongoServer(settings);

            const string databaseName = "smartstock";

            this._database = server.GetDatabase(databaseName);
        }

        public MongoCollection GetMongoCollection(string collectionName)
        {
            if (this._mongoCollections.ContainsKey(collectionName))
            {
                return this._mongoCollections[collectionName];
            }
            MongoCollection mongoCollection = this._database.GetCollection(collectionName);
            this._mongoCollections.Add(collectionName, mongoCollection);
            return mongoCollection;
        }

        public MongoCollection GetMongoCollection(Type type)
        {
            if (this._mongoCollections.ContainsKey(type.Name))
            {
                return this._mongoCollections[type.Name];
            }

            MongoCollection mongoCollection = this._database.GetCollection(type.Name);
            this._mongoCollections.Add(type.Name, mongoCollection);
            return mongoCollection;
        }
    }
}
