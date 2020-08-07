using Capstone.API;
using Capstone.DAO;
using System;
using System.Collections.Generic;
using System.IO;
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
                Dictionary<string, string> stockList = FileReadingMethod();
                foreach (KeyValuePair<string, string> keyValuePair in stockList)
                {
                    companySql.AddStock(stockAPI.GetCompanyStockInfo(keyValuePair.Key, keyValuePair.Value));
                    
                }
                
        }


        private Dictionary<string, string> FileReadingMethod()
        {
            Dictionary<string, string> stockInfo = new Dictionary<string, string>();
            string directory = @"..";
            string filename = @"database\Top50Stock.csv";

            string fullPath = Path.Combine(directory, filename);
            using (StreamReader rdr = new StreamReader(fullPath))
            {
                while (!rdr.EndOfStream)
                {
                    string line = rdr.ReadLine();
                    string[] itemsInfo = line.Split(",");
                    stockInfo.Add(itemsInfo[0], itemsInfo[1]);
                }
            }
            return stockInfo;
        }
    }
}
