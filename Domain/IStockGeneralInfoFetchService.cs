using System.Collections.Generic;
using Domain.Model;

namespace Domain
{
    public interface IStockGeneralInfoFetchService
    {
        IEnumerable<Stock> GeneralStocksGeneralInfo();
    }
}
