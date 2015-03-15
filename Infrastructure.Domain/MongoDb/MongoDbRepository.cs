using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Infrastructure.Domain.Model;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;


namespace Infrastructure.Domain.MongoDb
{
    public class MongoDbRepository<TAggregateRoot> : BaseRepository<TAggregateRoot>
        where TAggregateRoot : IAggregateRoot
    {
        private readonly IMongoDbContext _dbContext;

        public MongoDbRepository(IMongoDbContext dbContext, IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
            this._dbContext = dbContext;
        }

        #region IUnitOfWorkRepository Implementation

        public override void PersistNewItem(IAggregateRoot item)
        {
            MongoCollection collection = this._dbContext.GetMongoCollection(typeof(TAggregateRoot));
            TAggregateRoot entity = (TAggregateRoot)item;
            collection.Insert(entity);
        }

        public override void PersistUpdateItem(IAggregateRoot item)
        {
            MongoCollection collection = this._dbContext.GetMongoCollection(typeof(TAggregateRoot));
            TAggregateRoot entity = (TAggregateRoot)item;
            collection.Save(entity);
        }

        public override void PersistRemoveItem(IAggregateRoot item)
        {
            MongoCollection collection = this._dbContext.GetMongoCollection(typeof(TAggregateRoot));
            IMongoQuery query = Query.EQ("_id", item.Id);
            collection.Remove(query);
        }

        #endregion

        #region IRepository<TAggregateRoot> Implementation

        public override TAggregateRoot GetByKey(Guid key)
        {
            MongoCollection collection = this._dbContext.GetMongoCollection(typeof(TAggregateRoot));
            return collection.AsQueryable<TAggregateRoot>().First(doc => doc.Id == key);
        }

        public override IEnumerable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, bool>> conditionExpression)
        {
            MongoCollection collection = this._dbContext.GetMongoCollection(typeof(TAggregateRoot));
            return collection.AsQueryable<TAggregateRoot>().Where(conditionExpression);
        }

       
        public override bool Exist(TAggregateRoot aggregateRoot)
        {
            MongoCollection collection = this._dbContext.GetMongoCollection(typeof(TAggregateRoot));
            return collection.AsQueryable<TAggregateRoot>().Any(doc => doc.Id == aggregateRoot.Id);
        }

        #endregion
    }
}
