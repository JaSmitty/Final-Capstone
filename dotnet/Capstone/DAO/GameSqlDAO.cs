using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class GameSqlDAO : IGameDAO
    {
        private string connectionString;

        public GameSqlDAO(string dbconnectionString)
        {
            this.connectionString = dbconnectionString;
        }


        public UserGame AddUserToGame(int addedUserId, int gameId)
        {
            UserGame userGame = new UserGame(addedUserId, gameId);
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                }
            }
            catch
            {

            }
        }

        public Game CreateGame(int creatorId, string gameName, DateTime endDate)
        {
            Game newGame = new Game(creatorId, gameName, endDate);
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                }
            }
            catch
            {

            }
        }

        public Game GetGameByUserId(int userId)
        {
            UserGame userGame;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                }
            }
            catch
            {

            }
        }
    }
}
