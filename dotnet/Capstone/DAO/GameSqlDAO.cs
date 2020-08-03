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


        public UserGame AddUserToGame(int addedUserId, int gameId)
        {
            UserGame userGame = new UserGame(addedUserId, gameId);
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT into users_game(user_id, game_id, balance) VALUES (@organizer, @name, @endDate); Select @@identity;", conn);
                }
            }
            catch
            {

            }
            return userGame;
        }

        public Game CreateGame(int creatorId, string gameName, DateTime endDate)
        {
            Game newGame = new Game();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT into game(organizer, name, end_date, starting_balance) VALUES (@organizer, @name, @endDate, @startingBalance); Select @@identity;", conn);
                    cmd.Parameters.AddWithValue("@organizer", newGame.Organizer_ID);
                    cmd.Parameters.AddWithValue("@name", newGame.Name);
                    cmd.Parameters.AddWithValue("@endDate", newGame.End_Date);
                    cmd.Parameters.AddWithValue("@startingBalance", newGame.End_Date);
                    newGame.Game_ID = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch
            {

            }
            return newGame;
        }

        public List<Game> GetGamesByUserId(int userId)
        {
            List<Game> games = new List<Game>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT game.id, game.name, organizer, end_date, balance, users.username from game join users_game on users_game.game_id = game.id join users on users.id = game.organizer where users_game.users_id = @userId", conn);
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
            game.Game_ID = Convert.ToInt32(rdr["id"]);
            game.Name = Convert.ToString(rdr["name"]);
            game.Organizer_Name = Convert.ToString(rdr["username"]);
            game.Organizer_ID = Convert.ToInt32(rdr["organizer"]);
            game.End_Date = Convert.ToDateTime(rdr["end_date"]);
            game.Balance = Convert.ToDecimal(rdr["balance"]);
            return game;
        }
    }
}
