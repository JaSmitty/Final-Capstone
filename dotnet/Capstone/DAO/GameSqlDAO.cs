using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class GameSqlDAO
    {
        private string connectionString;

        public GameSqlDAO(string dbconnectionString)
        {
            this.connectionString = dbconnectionString;
        }


        //public UserGame AddUserToGame(int addedUserId, int gameId)
        //{
        //    UserGame userGame = new UserGame(addedUserId, gameId);
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();
        //            SqlCommand cmd = new SqlCommand("INSERT into users_game(user_id, game_id, balance) VALUES (@organizer, @name, @endDate); Select @@identity;", conn);
        //        }
        //    }
        //    catch
        //    {

        //    }
        //    return userGame;
        //}

        public int CreateGame(Game newGame)
        {
            int newGameID = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT into game(organizer_id, name, end_date) VALUES (@organizer, @name, @endDate); Select @@identity;", conn);
                    cmd.Parameters.AddWithValue("@organizer", newGame.OrganizerId);
                    cmd.Parameters.AddWithValue("@name", newGame.Name);
                    cmd.Parameters.AddWithValue("@endDate", newGame.EndDate);
                    newGameID= Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch
            {

            }
            return newGameID;
        }

        public List<Game> GetGamesByUserId(int userId)
        {
            List<Game> games = new List<Game>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT game.id, game.name, organizer_id, end_date, balance, users.username from game join users_game on users_game.game_id = game.id join users on users.id = game.organizer_id where users_game.users_id = @userId", conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        games.Add(ConvertReaderToGame(rdr));
                    }
                }
            }
            catch
            {

            }
            return games;
        }

        private Game ConvertReaderToGame(SqlDataReader rdr)
        {
            Game game = new Game();
            game.GameId = Convert.ToInt32(rdr["id"]);
            game.Name = Convert.ToString(rdr["name"]);
            game.OrganizerName = Convert.ToString(rdr["username"]);
            game.OrganizerId = Convert.ToInt32(rdr["organizer_id"]);
            game.EndDate = Convert.ToDateTime(rdr["end_date"]);
            game.Balance = Convert.ToDecimal(rdr["balance"]);
            return game;
        }
    }
}
