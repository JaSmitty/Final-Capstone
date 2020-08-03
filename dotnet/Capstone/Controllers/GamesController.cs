﻿using System;
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
        public ActionResult<int> CreateGame(Game newGame, int userId)
        {
            try
            {
                string location = $"api/games/{userId}/{newGame.Game_ID}";
                return Created( location, gameSqlDAO.CreateGame(newGame));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}