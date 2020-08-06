using Capstone.API;
using Capstone.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.DataLoops
{
    public class FinnHubDataLoop
    {
        StockAPI stockAPI = new StockAPI();
        private readonly CompanySqlDAO companySqlDAO;
        public FinnHubDataLoop(CompanySqlDAO companySql)
        {
            this.companySqlDAO = companySql;
        }

        public void Run()
        {
            while (true)
            {
                companySqlDAO.AddStock(stockAPI.GetCompanyStockInfo("AAPL"));
                Thread.Sleep(60000);
            }
        }

        
    }
}
