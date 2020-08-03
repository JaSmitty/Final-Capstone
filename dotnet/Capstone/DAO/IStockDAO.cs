using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public interface IStockDAO
    {
        Company GetStockByName(string stockTick);
        Company AddStock(string stockTick, decimal openPrice, decimal highPrice, decimal lowPrice, decimal currentPrice, decimal previousClosePrice);


    }
}
