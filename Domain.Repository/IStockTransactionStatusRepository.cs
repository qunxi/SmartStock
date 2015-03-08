using Domain.Model;

namespace Domain.Repository
{
    public interface IStockTransactionStatusRepository
    {
        void SaveStockStatus(StockTransactionStatus status);
    }
}
