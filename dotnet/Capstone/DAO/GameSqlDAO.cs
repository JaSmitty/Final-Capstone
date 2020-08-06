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


        public bool AddUsersToGame(List<UserGame> userGame)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    foreach (UserGame user in userGame)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT into users_game(users_id, game_id, balance) VALUES (@userId, @gameId, @balance);", conn);
                        cmd.Parameters.AddWithValue("@userId", user.UserId);
                        cmd.Parameters.AddWithValue("@gameId", user.GameId);

                        /******* Do we have balance be set or hard code it here?*******/
                        cmd.Parameters.AddWithValue("@balance", (decimal)100000);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                throw;
            }
            return true;
        }

        public Game CreateGame(Game game)
        {
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
                    cmd.Parameters.AddWithValue("@balance", game.Balance);
                    game.GameId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch
            {
                throw;
            }
            return game;
        }

        public List<Game> GetGamesByUserName(string username)
        {
            List<Game> games = new List<Game>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT game.id, game.name, organizer_id, end_date, balance, uORGANIZER.username FROM game
JOIN users_game ON game.id = users_game.game_id
JOIN users uPLAY ON uPLAY.id = users_game.users_id
JOIN users uORGANIZER ON game.organizer_id = uORGANIZER.id
WHERE uPLAY.username = @username", conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        games.Add(ReadToGame(rdr));
                    }
                }
            }
            catch
            {
                throw;
            }
            return games;
        }
        public List<UserInfo> GetUsersToInviteToGame(int gameId)
        {
            List<UserInfo> userList = new List<UserInfo>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"SELECT id, username FROM users WHERE users.id NOT IN
(SELECT u.id FROM users u
LEFT JOIN users_game ug ON u.id = ug.users_id
WHERE game_id = @gameId)", conn);
                cmd.Parameters.AddWithValue("@gameId", gameId);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    userList.Add(ReadToUserInfo(reader));
                }
            }
            return userList;
        }
        private Game ReadToGame(SqlDataReader rdr)
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
        private UserInfo ReadToUserInfo(SqlDataReader rdr)
        {
            UserInfo user = new UserInfo();

            user.UserId = Convert.ToInt32(rdr["id"]);
            user.Username = Convert.ToString(rdr["username"]);

            return user;
        }
    }
}
