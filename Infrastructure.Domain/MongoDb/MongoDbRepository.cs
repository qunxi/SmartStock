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
    public class MongoDbRepository<TAggregateRoot> : IRepository<TAggregateRoot>, IUnitOfWorkRepository
        where TAggregateRoot : IAggregateRoot
    {
        private readonly IMongoDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;

        public MongoDbRepository(IMongoDbContext dbContext, IUnitOfWork unitOfWork)
        {
            this._dbContext = dbContext;
            this._unitOfWork = unitOfWork;
        }

        #region Wrapper MongoDbContext Implementation

        public void RegisterAdd(IAggregateRoot entity) 
        {
            this._unitOfWork.RegisterAdd(entity, this);
        }

        public void RegisterUpdate(IAggregateRoot entity)
        {
            this._unitOfWork.RegisterUpdate(entity, this);
        }

        public void RegisterRemoved(IAggregateRoot entity)
        {
            this._unitOfWork.RegisterRemoved(entity, this);
        }

        #endregion

        #region IUnitOfWorkRepository Implementation

        public void PersistNewItem(IAggregateRoot item)
        {
            MongoCollection collection = this._dbContext.GetMongoCollection(typeof(TAggregateRoot));
            TAggregateRoot entity = (TAggregateRoot)item;
            collection.Insert(entity);
        }

        public void PersistUpdateItem(IAggregateRoot item)
        {
            MongoCollection collection = this._dbContext.GetMongoCollection(typeof(TAggregateRoot));
            TAggregateRoot entity = (TAggregateRoot)item;
            collection.Save(entity);
        }

        public void PersistRemoveItem(IAggregateRoot item)
        {
            MongoCollection collection = this._dbContext.GetMongoCollection(typeof(TAggregateRoot));
            IMongoQuery query = Query.EQ("_id", item.Id);
            collection.Remove(query);
        }

        #endregion

        #region IRepository<TAggregateRoot> Implementation

        public TAggregateRoot GetByKey(Guid key)
        {
            MongoCollection collection = this._dbContext.GetMongoCollection(typeof(TAggregateRoot));
            return collection.AsQueryable<TAggregateRoot>().First(doc => doc.Id == key);
        }

        public IEnumerable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, bool>> conditionExpression)
        {
            MongoCollection collection = this._dbContext.GetMongoCollection(typeof(TAggregateRoot));
            return collection.AsQueryable<TAggregateRoot>().Where(conditionExpression);
        }

        public void Add(TAggregateRoot aggregateRoot)
        {
            this._unitOfWork.RegisterAdd(aggregateRoot, this);
        }

        public bool Exist(TAggregateRoot aggregateRoot)
        {
            MongoCollection collection = this._dbContext.GetMongoCollection(typeof(TAggregateRoot));
            return collection.AsQueryable<TAggregateRoot>().Any(doc => doc.Id == aggregateRoot.Id);
        }

        public void Update(TAggregateRoot aggregateRoot)
        {
            this._unitOfWork.RegisterUpdate(aggregateRoot, this);
        }

        public void Remove(TAggregateRoot aggregateRoot)
        {
            this._unitOfWork.RegisterRemoved(aggregateRoot, this);
        }

        #endregion
    }
}
