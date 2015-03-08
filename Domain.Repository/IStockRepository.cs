namespace Domain.Repository
{
    using Domain.Model;

    public interface IStockRepository
    {
        void SaveStock(Stock stock);

        void UpdateStock(Stock stock);

        void RemoveStock(Stock stock);
    }
}
