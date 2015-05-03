using Domain.Model.Stocks;
using Infrastructure.Domain;

namespace Domain.Repository
{
    public interface IStockRepository : IRepository<Stock>
    {
        bool IsStockExist(Stock stock);
        Stock FindStockByCode(string code);
    }
}
