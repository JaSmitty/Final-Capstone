using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Capstone.API;
using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration;

namespace Capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly StockSqlDAO stockSqlDAO;
        private readonly StockAPI stockAPI;
        private List<string> stockTickers;
        public StocksController(StockSqlDAO stockSqlDAO, StockAPI stockAPI)
        {
            this.stockSqlDAO = stockSqlDAO;
            this.stockAPI = stockAPI;
            this.stockTickers = ReadToStocks();
        }
        [HttpGet]
        public ActionResult<List<Stock>> GetCurrentStocks()
        {
            try
            {
                return Ok(stockSqlDAO.GetCurrentStocks(stockTickers));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //[HttpGet]
        //[Route("{stockTicker}")]
        //public ActionResult<Company> GetStockByTickerName(string stockTicker)
        //{
        //    try
        //    {
        //        return Ok(StockAPI.GetCompanyStockInfo(stockTicker));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}
        //[HttpPost]
        //[Route]

        private List<string> ReadToStocks()
        {
            List<string> stockInfo = new List<string>();
            string directory = @"..";
            string filename = @"database\Top50Stock.csv";

            string fullPath = Path.Combine(directory, filename);
            using (StreamReader rdr = new StreamReader(fullPath))
            {
                while (!rdr.EndOfStream)
                {
                    string line = rdr.ReadLine();
                    string[] itemsInfo = line.Split(",");
                    stockInfo.Add(itemsInfo[0]);
                }
            }
            return stockInfo;
        }
    }
}
