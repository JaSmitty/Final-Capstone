using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly CompanySqlDAO CompanySqlDao;
        public CompanyController(CompanySqlDAO companySqlDAO)
        {
            this.CompanySqlDao = companySqlDAO;
        }


        [HttpGet]
        [Route("{stockTicker}")]
        public ActionResult<Company> GetStockByTickerName(string stockTicker)
        {
            try
            {
                return Ok(CompanySqlDao.GetCurrentStockByName(stockTicker));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        [Route]
    }
}
