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


        public bool AddUserToGame(UserGame userGame)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT into users_game(users_id, game_id, balance) VALUES (@userId, @gameId, @balance);", conn);
                    cmd.Parameters.AddWithValue("@userId", userGame.UserId);
                    cmd.Parameters.AddWithValue("@gameId", userGame.GameId);

                    /******* Do we have balance be set or hard code it here?*******/
                    cmd.Parameters.AddWithValue("@balance", (decimal)100000);
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
            return true;
        }

        public int CreateGame(Game game)
        {
            int newGameID = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    const string QUERY = @"Begin Transaction INSERT into game(organizer_id, name, end_date) VALUES (@organizer_id, @name, @endDate); Select @@identity
INSERT into users_game(users_id, game_id, balance) VALUES (@organizer_id, @@identity, @balance) Commit Transaction";
                    SqlCommand cmd = new SqlCommand(QUERY, conn);
                    cmd.Parameters.AddWithValue("@organizer_id", game.OrganizerId);
                    cmd.Parameters.AddWithValue("@name", game.Name);
                    cmd.Parameters.AddWithValue("@endDate", game.EndDate);
                    cmd.Parameters.AddWithValue("@balance", 100000M);
                    newGameID = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch
            {
                throw;
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
