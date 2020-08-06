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
        CompanySqlDAO companySql = new CompanySqlDAO("Server=.\\SQLEXPRESS;Database=final_capstone;Trusted_Connection=True;");
        //public FinnHubDataLoop()
        //{
        //    this.companySqlDAO = companySql;
        //}

        public void Run()
        {
            while (true)
            {
                companySql.AddStock(stockAPI.GetCompanyStockInfo("AAPL"));
                Thread.Sleep(60000);
            }
        }

        
    }
}
