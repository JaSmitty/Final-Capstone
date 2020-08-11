using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Capstone.API;
using Capstone.DAO;
using Capstone.DataLoops;
using Capstone.Models;
using Hangfire;
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
        private FinnHubDataLoop DataLoop;
        private readonly BuySellSqlDAO BuySellDAO;
        private string Username
        {
            get
            {
                return User?.Identity?.Name;
            }
        }
        public StocksController(StockSqlDAO stockSqlDAO, StockAPI stockAPI, FinnHubDataLoop dataLoop, BuySellSqlDAO buySellDAO)
        {
            this.stockSqlDAO = stockSqlDAO;
            this.stockAPI = stockAPI;
            this.stockTickers = ReadToStocks();
            this.DataLoop = dataLoop;
            this.BuySellDAO = buySellDAO;
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


        //Call this get at server startup to get hangfire to start
        //[HttpGet]
        [Route("sendit")]
        public void HangfireSetup()
        {
            RecurringJob.AddOrUpdate(recurringJobId: "Dataloop", methodCall: () => DataLoop.Run(), "*/10 * * * *");
        }


        [HttpPost]
        [Route("buy")]
        public ActionResult<BuyModel> BuyStock(BuyModel buyModel)
        {
            try
            {
                int id = this.BuySellDAO.GetUserId(this.Username);
                buyModel.UsersId = id;
                BuyModel returnModel = this.BuySellDAO.BuyStock(buyModel);
                return Created($"api/stocks/buy/{returnModel.CompanyTicker}", returnModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("sell")]
        public ActionResult<SellModel> SellStock(SellModel sellModel)
        {
            try
            {
                SellModel returnModel = this.BuySellDAO.SellStock(sellModel);
                return Created($"api/stocks/sell/{returnModel.StockAtSellId}", returnModel);
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
