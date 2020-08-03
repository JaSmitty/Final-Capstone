using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public interface IStockDAO
    {
        Stock GetStockByName(string stockTick);
        Stock AddStock(string stockTick, decimal openPrice, decimal highPrice, decimal lowPrice, decimal currentPrice, decimal previousClosePrice);

    }
}
