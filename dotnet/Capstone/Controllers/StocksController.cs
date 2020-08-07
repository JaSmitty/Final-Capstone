using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.API;
using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly CompanySqlDAO companySqlDao;
        private readonly StockAPI stockAPI;
        public StocksController(CompanySqlDAO companySqlDAO, StockAPI stockAPI)
        {
            this.companySqlDao = companySqlDAO;
            this.stockAPI = stockAPI;
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
    }
}
