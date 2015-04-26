using Domain.Model.Stocks;

namespace Domain.Repository
{
    public interface ITransactionStatusRepository
    {
        string CollectionName { get; set; }

        void Add(TransactionStatus transStatus);
    }
}
