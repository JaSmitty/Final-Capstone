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
    [Route("users/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserSqlDAO UserDAO;

        public UserController(UserSqlDAO userDAO)
        {
            this.UserDAO = userDAO;
        }

        [HttpGet]
        [Route("{userId}")]
        public ActionResult<List<UserInfo>> GetAllOtherUsers(int userId)
        {
            try
            {
                return Ok(UserDAO.GetAllOtherUsers(userId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
