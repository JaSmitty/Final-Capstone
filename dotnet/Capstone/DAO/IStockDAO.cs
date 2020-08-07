using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public interface IStockDAO
    {
        Stock GetCurrentStockByName(string stockTick);
        int AddStock(Stock company);


    }
}
