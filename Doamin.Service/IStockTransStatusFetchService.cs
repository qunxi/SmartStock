using System.Collections.Generic;
using Domain.Model;

namespace Domain.Service
{
    public interface IStockTransStatusFetchService
    {
        IEnumerable<StockTransactionStatus> GetStocksByDateScope(DateScope dateScope);
    }
}
