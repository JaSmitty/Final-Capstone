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
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserSqlDAO userSqlDAO;

        public UsersController(UserSqlDAO userSqlDAO)
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
