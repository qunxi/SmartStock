using System.Collections.Generic;
using Domain.Model;

namespace Domain
{
    public interface IStockTransStatusFetchService
    {
        IEnumerable<StockTransactionStatus> GetStocksByDateScope(DateScope dateScope);
    }
}
