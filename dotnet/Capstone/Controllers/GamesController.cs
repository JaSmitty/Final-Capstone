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
        private string UserName
        {
            get
            {
                return User?.Identity?.Name;
            }
        }
        public GamesController(GameSqlDAO gameSqlDAO)
        {
            this.gameSqlDAO = gameSqlDAO;
        }

        [HttpGet]
        public ActionResult<List<Game>> GetActiveGamesByUserName()
        {
            try
            {
                return Ok(gameSqlDAO.GetActiveGamesByUserName(UserName));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<Game> CreateGame(Game game)
        {
            try
            {
                game = gameSqlDAO.CreateGame(game);
                string location = $"api/games/{game.GameId}";
                return Created(location, game);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        // TODO Write a controller method to receive a user ID and game ID to add to the users_game table
        // {userId}/ invite

        [HttpPost]
        [Route("invite")]
        public ActionResult<bool> AddUsersToGame(List<UserGame> userGames)
        {
            try
            {
                string location = $"api/games/invite/success";
                return Created(location, gameSqlDAO.AddUsersToGame(userGames));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        [Route("{gameId}/invite")]
        public ActionResult<List<UserInfo>> GetUsersToInvite(int gameId)
        {
            try
            {
                return Ok(gameSqlDAO.GetUsersToInviteToGame(gameId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        [Route("{gameId}/players")]
        public ActionResult<List<UserInfo>> GetPlayersInGame(int gameId)
        {
            try
            {
                return Ok(gameSqlDAO.GetPlayersInGame(gameId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}