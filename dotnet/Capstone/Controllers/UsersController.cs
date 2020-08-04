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
    public class UsersController : ControllerBase
    {
        private readonly IUserDAO userSqlDAO;

        public UsersController(IUserDAO userSqlDAO)
        {
            this.userSqlDAO = userSqlDAO;
        }

        [HttpGet]
        [Route("{userId}")]
        public ActionResult<List<UserInfo>> GetAllOtherUsers(int userId)
        {
            try
            {
                return Ok(userSqlDAO.GetAllOtherUsers(userId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
