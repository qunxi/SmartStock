using System.Collections.Generic;
using Domain.Model;

namespace Domain.Service
{
    public interface IStockGeneralInfoFetchService
    {
        IEnumerable<Stock> GeneralStocksGeneralInfo();
    }
}
