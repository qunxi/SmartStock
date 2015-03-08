using Domain.Model;

namespace Domain.Repository
{
    public interface IUnitOfWorkRepository
    {
        void PersistNewItem(IAggregateRoot item);

        void PersistUpdateItem(IAggregateRoot item);

        void PersistRemoveItem(IAggregateRoot item);
    }
}
