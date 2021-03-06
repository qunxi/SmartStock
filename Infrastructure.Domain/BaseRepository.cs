﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Infrastructure.Domain.Model;

namespace Infrastructure.Domain
{
    public abstract class BaseRepository<TAggregateRoot> : IRepository<TAggregateRoot>, IUnitOfWorkRepository
        where TAggregateRoot : IAggregateRoot
    {
        protected readonly IUnitOfWork _unitOfWork;

        protected BaseRepository(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        } 

        #region Wrapper UnitofWork Implementation

        protected void RegisterAdd(IAggregateRoot entity)
        {
            this._unitOfWork.RegisterAdd(entity, this);
        }

        protected void RegisterUpdate(IAggregateRoot entity)
        {
            this._unitOfWork.RegisterUpdate(entity, this);
        }

        protected void RegisterRemoved(IAggregateRoot entity)
        {
            this._unitOfWork.RegisterRemoved(entity, this);
        }

        #endregion

        public virtual void Add(TAggregateRoot aggregateRoot)
        {
            RegisterAdd(aggregateRoot);
        }

        public virtual void Update(TAggregateRoot aggregateRoot)
        {
            RegisterUpdate(aggregateRoot);
        }

        public virtual void Remove(TAggregateRoot aggregateRoot)
        {
            RegisterRemoved(aggregateRoot);
        }

        public abstract TAggregateRoot GetByKey(Guid key);
        public abstract TAggregateRoot Find(Expression<Func<TAggregateRoot, bool>> expression);

        public abstract IEnumerable<TAggregateRoot> FindAll();

        public abstract IEnumerable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, bool>> expression);

        public abstract IEnumerable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, bool>> expression,
            Expression<Func<TAggregateRoot, dynamic>> orderExpression, SortOrder sortOrder); 

        public abstract bool Exist(TAggregateRoot aggregateRoot);

        public abstract void PersistNewItem(IAggregateRoot item);

        public abstract void PersistUpdateItem(IAggregateRoot item);

        public abstract void PersistRemoveItem(IAggregateRoot item);
    }
}
