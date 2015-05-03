using System;
using Domain.Model.Stocks;

namespace Domain.Repository
{
    public interface ITransactionStatusRepository
    {
        // string CollectionName { get; }

        void AddStockTransactionStatus(Stock stock);

        DateTime GetLatestUpdatedInDb(Stock stock, DateTime startDate);
    }
}
