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
        private string Username
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
        public ActionResult<List<Game>> GetActiveGames()
        {
            try
            {
                return Ok(gameSqlDAO.GetActiveGames(Username));
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
        public ActionResult<bool> InviteUsersToGame(List<UserGame> userGames)
        {
            try
            {
                string location = $"api/games/invite/success";
                return Created(location, gameSqlDAO.InviteUsersToGame(userGames));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        [Route("{gameId}/invite")]
        public ActionResult<List<UserInfo>> GetUsersToInviteToGame(int gameId)
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
        public ActionResult<List<UserInfo>> GetActivePlayersInGame(int gameId)
        {
            try
            {
                return Ok(gameSqlDAO.GetActivePlayersInGame(gameId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        [Route("pending")]
        public ActionResult<List<Game>> GetPendingGames()
        {
            try
            {
                return Ok(gameSqlDAO.GetPendingGames(Username));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut]
        [Route("{gameId}/accept")]
        public ActionResult<bool> AcceptInvitation(UserGame userGame)
        {
            try
            {
                return Ok(gameSqlDAO.AcceptInvitation(userGame));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}