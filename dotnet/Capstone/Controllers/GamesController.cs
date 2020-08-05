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
    public class GamesController : ControllerBase
    {
        private readonly GameSqlDAO gameSqlDAO;
        public GamesController(GameSqlDAO gameSqlDAO)
        {
            this.gameSqlDAO = gameSqlDAO;
        }

        [HttpGet]
        [Route("{userId}")]
        public ActionResult<List<Game>> GetGamesByUserId(int userId)
        {
            try
            {
                return Ok(gameSqlDAO.GetGamesByUserId(userId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("{userId}")]
        public ActionResult<int> CreateGame(Game game, int userId)
        {
            try
            {
                string location = $"api/games/{userId}/{game.GameId}";
                return Created(location, gameSqlDAO.CreateGame(game));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        // TODO Write a controller method to receive a user ID and game ID to add to the users_game table
        // {userId}/ invite

        [HttpPost]
        [Route("{userId}/invite")]
        public ActionResult<bool> AddUsersToGame(List<UserGame> userGames, int userId)
        {
            try
            {
                string location = $"api/games/{userId}/invite";
                return Created(location, gameSqlDAO.AddUserToGame(userGames));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}