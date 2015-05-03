using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Infrastructure.Domain
{
    public enum SortOrder
    {
        Ascending,

        Descending
    }

    public interface IRepository<TAggregateRoot>
    {
        TAggregateRoot GetByKey(Guid key);
        TAggregateRoot Find(Expression<Func<TAggregateRoot, bool>> expression);

        IEnumerable<TAggregateRoot> FindAll(); 
        IEnumerable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, bool>> expression);

        IEnumerable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, bool>> expression,
            Expression<Func<TAggregateRoot, dynamic>> orderExpression,
            SortOrder sortOrder = SortOrder.Ascending); 
        
        void Add(TAggregateRoot aggregateRoot);
        bool Exist(TAggregateRoot aggregateRoot);
        void Update(TAggregateRoot aggregateRoot);
        void Remove(TAggregateRoot aggregateRoot);
    }
}
