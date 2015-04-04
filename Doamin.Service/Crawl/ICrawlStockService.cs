﻿using System.Collections.Generic;
using Domain.Model;
using Domain.Model.Stocks;

namespace Domain.Service.Crawl
{
    public interface ICrawlStockService
    {
        IEnumerable<Stock> GetAllStocksList();
        Stock GetStockTransStatusByDate(Stock stock, DateScope dateScope);
    }
}
