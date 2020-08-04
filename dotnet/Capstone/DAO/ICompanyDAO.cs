using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public interface ICompanyDAO
    {
        Company GetCurrentStockByName(string stockTick);
        int AddStock(Company company);


    }
}
