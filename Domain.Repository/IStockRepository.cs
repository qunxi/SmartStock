using Domain.Model;

namespace Domain.Repository
{
    public interface IStockRepository
    {
        void SaveStock(Stock stock);

        void UpdateStock(Stock stock);

        void RemoveStock(Stock stock);
    }
}
