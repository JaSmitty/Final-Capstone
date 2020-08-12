using Capstone.DAO;
using Capstone.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DataLoops
{
    public class CheckForGameEnd
    {
        private string connectionString;
        public CheckForGameEnd(string dbconnectionString)
        {
            this.connectionString = dbconnectionString;
        }

        private StockSqlDAO stockDAO;

        public void Run()
        {
            List<int> gamesToEnd = CheckForTheEnd();
            foreach(int gameId in gamesToEnd) 
            {
                stockDAO.GameEndSell(gameId);
            }
        }


        public List<int> CheckForTheEnd()
        {
            List<Game> games = new List<Game>();
            List<int> gamesToEnd = new List<int>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM game", conn);
                SqlDataReader rdr = cmd.ExecuteReader();
                while ( rdr.Read())
                {
                    games.Add(ReadToGame(rdr));
                }
                foreach (Game game in games)
                {
                    if(game.EndDate == DateTime.Today && !game.IsComplete)
                    {
                        gamesToEnd.Add(game.GameId);
                        CompleteGame(game.GameId);
                    }
                }
            }
            return gamesToEnd;
        }
        private void CompleteGame(int gameId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"Update game
                                                    set is_complete = 1
                                                    where id = @gameId", conn);
                cmd.Parameters.AddWithValue("@gameId", gameId);
                cmd.ExecuteNonQuery();
                
            }
        }

        private Game ReadToGame(SqlDataReader rdr)
        {
            Game game = new Game();
            game.GameId = Convert.ToInt32(rdr["id"]);
            game.Name = Convert.ToString(rdr["name"]);
            game.OrganizerId = Convert.ToInt32(rdr["organizer_id"]);
            game.StartDate = Convert.ToDateTime(rdr["start_date"]);
            game.EndDate = Convert.ToDateTime(rdr["end_date"]);
            game.IsComplete = Convert.ToBoolean(rdr["is_complete"]);
            return game;
        }
    }
}
