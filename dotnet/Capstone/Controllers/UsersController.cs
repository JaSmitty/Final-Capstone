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
        [Route("invite/{gameId}")]
        public ActionResult<List<UserInfo>> GetUsersToInvite(int gameId)
        {
            try
            {
                return Ok(userSqlDAO.GetUsersToInvite(gameId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
