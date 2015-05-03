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
        protected readonly IMongoDbContext _dbContext;

        public MongoDbRepository(IMongoDbContext dbContext, IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
            this._dbContext = dbContext;
        }

        #region IUnitOfWorkRepository Implementation

        public override void PersistNewItem(IAggregateRoot item)
        {
            MongoCollection collection = GetMongoCollection();
            TAggregateRoot entity = (TAggregateRoot)item;
            collection.Insert(entity);
        }

        public override void PersistUpdateItem(IAggregateRoot item)
        {
            MongoCollection collection = GetMongoCollection();
            TAggregateRoot entity = (TAggregateRoot)item;
            collection.Save(entity);
        }

        public override void PersistRemoveItem(IAggregateRoot item)
        {
            MongoCollection collection = GetMongoCollection();
            IMongoQuery query = Query.EQ("_id", item.Id);
            collection.Remove(query);
        }

        #endregion IUnitOfWorkRepository Implementation

        #region IRepository<TAggregateRoot> Implementation

        public override IEnumerable<TAggregateRoot> FindAll()
        {
            MongoCollection collection = GetMongoCollection();
            return collection.AsQueryable<TAggregateRoot>();
        }

        public override TAggregateRoot GetByKey(Guid key)
        {
            MongoCollection collection = GetMongoCollection();
            return collection.AsQueryable<TAggregateRoot>().First(doc => doc.Id == key);
        }

        public override TAggregateRoot Find(Expression<Func<TAggregateRoot, bool>> expression)
        {
            return FindAll(expression).FirstOrDefault();
        }

        public override IEnumerable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, bool>> expression,
                                    Expression<Func<TAggregateRoot, dynamic>> orderExpression, SortOrder sortOrder)
        {
            MongoCollection collection = GetMongoCollection();
            var query = collection.AsQueryable<TAggregateRoot>().Where(expression);
            switch (sortOrder)
            {
                case SortOrder.Ascending:
                    return  query.OrderBy(orderExpression);
                   
                case SortOrder.Descending:
                    return query.OrderByDescending(orderExpression);
            }
            return query;
        }

        public override IEnumerable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, bool>> conditionExpression)
        {
            MongoCollection collection = GetMongoCollection();
            return collection.AsQueryable<TAggregateRoot>().Where(conditionExpression);
        }

       
        public override bool Exist(TAggregateRoot aggregateRoot)
        {
            MongoCollection collection = GetMongoCollection();
            return collection.AsQueryable<TAggregateRoot>().Any(doc => doc.Id == aggregateRoot.Id);
        }

        #endregion IRepository<TAggregateRoot> Implementation

        protected virtual MongoCollection GetMongoCollection()
        {
            return this._dbContext.GetMongoCollection(typeof (TAggregateRoot));
        }
    }
}
