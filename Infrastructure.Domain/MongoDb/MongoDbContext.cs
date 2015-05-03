using System;
using System.Collections.Generic;
using MongoDB.Driver;

namespace Infrastructure.Domain.MongoDb
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly MongoDatabase database;
        private readonly Dictionary<string, MongoCollection> mongoCollections; 
 
        public MongoDbContext(string databaseName)
        {
            this.mongoCollections = new Dictionary<string, MongoCollection>();

            MongoServerSettings settings = new MongoServerSettings
            {
               Server = new MongoServerAddress("localhost")
            };

            MongoServer server = new MongoServer(settings);

            //const string databaseName = "smartstock";

            this.database = server.GetDatabase(databaseName);
        }

        public MongoCollection GetMongoCollection(string collectionName)
        {
            if (this.mongoCollections.ContainsKey(collectionName))
            {
                return this.mongoCollections[collectionName];
            }
            MongoCollection mongoCollection = this.database.GetCollection(collectionName);
            this.mongoCollections.Add(collectionName, mongoCollection);
            return mongoCollection;
        }

        public MongoCollection GetMongoCollection(Type type)
        {
            if (this.mongoCollections.ContainsKey(type.Name))
            {
                return this.mongoCollections[type.Name];
            }

            MongoCollection mongoCollection = this.database.GetCollection(type.Name);
            this.mongoCollections.Add(type.Name, mongoCollection);
            return mongoCollection;
        }
    }
}
