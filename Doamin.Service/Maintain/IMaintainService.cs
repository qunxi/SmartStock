using System;
using System.Collections.Generic;
using Domain.Model.Stocks;

namespace Domain.Service.Maintain
{
    public interface IMaintainService
    {
        // bool IsUninitialDatabase();
       
        void InitialAllStocksHistory();
        void UpdateAllStocksHistory(DateTime start, DateTime end);
        void RemoveStockHistory(string stockCode);
    }
}
