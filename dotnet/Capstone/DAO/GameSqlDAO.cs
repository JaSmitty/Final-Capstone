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


        public bool InviteUsersToGame(List<UserGame> userGame, string username)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    if (!CheckThatUserIsInGame(userGame, username, conn))
                    {
                        return false;
                    }
                    foreach (UserGame user in userGame)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT into users_game(users_id, game_id, balance) VALUES (@userId, @gameId, @balance);", conn);
                        cmd.Parameters.AddWithValue("@userId", user.UserId);
                        cmd.Parameters.AddWithValue("@gameId", user.GameId);

                        cmd.Parameters.AddWithValue("@balance", 0M);
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

        private bool CheckThatUserIsInGame(List<UserGame> userGame, string username, SqlConnection conn)
        {
            List<int> gamesIds = new List<int>();
            conn.Open();
            string QUERY = @"SELECT id FROM users WHERE username = @username";
            SqlCommand cmd = new SqlCommand(QUERY, conn);
            cmd.Parameters.AddWithValue("@username", username);
            int id = Convert.ToInt32(cmd.ExecuteScalar());

            QUERY = @"SELECT game_id FROM users_game WHERE users_id = @userId";
            cmd = new SqlCommand(QUERY, conn);
            cmd.Parameters.AddWithValue("@userId", id);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                gamesIds.Add(Convert.ToInt32(rdr["game_id"]));
            }
            foreach (UserGame user in userGame)
            {
                if (!gamesIds.Exists(gameId => gameId == user.GameId))
                {
                    return false;
                }
            }
            rdr.Close();
            return true;
        }

        public Game CreateGame(Game game, string username)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string QUERY = @"SELECT id FROM users WHERE username = @username";
                    SqlCommand cmd = new SqlCommand(QUERY, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    int userId = Convert.ToInt32(cmd.ExecuteScalar());
                    if (userId != game.OrganizerId)
                    {
                        return null;
                    }
                    QUERY = @"Begin Transaction INSERT into game(organizer_id, name, start_date, end_date) VALUES (@organizer_id, @name, @startDate, @endDate); Select @@identity
                    INSERT into users_game(users_id, game_id, status, balance) VALUES (@organizer_id, @@identity, 'approved', @balance) Commit Transaction";
                    cmd = new SqlCommand(QUERY, conn);
                    cmd.Parameters.AddWithValue("@organizer_id", game.OrganizerId);
                    cmd.Parameters.AddWithValue("@name", game.Name);
                    cmd.Parameters.AddWithValue("@startDate", game.StartDate);
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
        public Game GetGameById(string username, int gameId)
        {
            try
            {
                Game game = new Game();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    const string QUERY = @"SELECT game.id, game.name, organizer_id, start_date, end_date, balance, uORGANIZER.username FROM game
JOIN users_game ON game.id = users_game.game_id
JOIN users uPLAY ON uPLAY.id = users_game.users_id
JOIN users uORGANIZER ON game.organizer_id = uORGANIZER.id
WHERE uPLAY.username = @username AND users_game.status = 'approved' AND game.id = @gameId";
                    SqlCommand cmd = new SqlCommand(QUERY, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@gameId", gameId);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        game = ReadToGame(rdr);
                    }
                    return game;
                }
            }
            catch
            {
                throw;
            }
        }
        public List<Game> GetActiveGames(string username)
        {
            List<Game> games = new List<Game>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT game.id, game.name, organizer_id, start_date, end_date, balance, uORGANIZER.username FROM game
JOIN users_game ON game.id = users_game.game_id
JOIN users uPLAY ON uPLAY.id = users_game.users_id
JOIN users uORGANIZER ON game.organizer_id = uORGANIZER.id
WHERE uPLAY.username = @username AND users_game.status = 'approved' AND GETDATE() <= end_date", conn);
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
        public List<Game> GetCompletedGames(string username)
        {
            List<Game> games = new List<Game>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT game.id, game.name, organizer_id, start_date, end_date, balance, uORGANIZER.username FROM game
JOIN users_game ON game.id = users_game.game_id
JOIN users uPLAY ON uPLAY.id = users_game.users_id
JOIN users uORGANIZER ON game.organizer_id = uORGANIZER.id
WHERE uPLAY.username = @username AND users_game.status = 'approved' AND GETDATE() > end_date", conn);
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
JOIN users_game ug ON u.id = ug.users_id
WHERE game_id = @gameId)", conn);
                cmd.Parameters.AddWithValue("@gameId", gameId);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    UserInfo user = new UserInfo();
                    user.UserId = Convert.ToInt32(reader["id"]);
                    user.Username = Convert.ToString(reader["username"]);
                    userList.Add(user);
                }
            }
            return userList;
        }
        public List<UserInfo> GetActivePlayersInGame(int gameId)
        {
            List<UserInfo> userList = new List<UserInfo>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"SELECT u.id, u.username, ug.balance FROM users u
JOIN users_game ug ON u.id = ug.users_id
WHERE game_id = @gameId AND ug.status = 'approved'", conn);
                cmd.Parameters.AddWithValue("@gameId", gameId);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    userList.Add(ReadToUserInfo(reader));
                }
                reader.Close();
                foreach(UserInfo user in userList)
                {
                    user.TotalWorth = user.Balance;
                    Dictionary<string, double> sharesPerCompany = new Dictionary<string, double>();
                    cmd = new SqlCommand(@"SELECT ticker, SUM(shares_currently_owned) AS shares FROM buy_table
JOIN company ON buy_table.stock_at_buy_id = company.stock_id
WHERE users_id = @userId AND game_id = @gameId
GROUP BY ticker", conn);
                    cmd.Parameters.AddWithValue("@gameId", gameId);
                    cmd.Parameters.AddWithValue("@userId", user.UserId);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string ticker = Convert.ToString(reader["ticker"]);
                        double shares = Convert.ToDouble(reader["shares"]);
                        sharesPerCompany.Add(ticker, shares);
                    }
                    reader.Close();
                    foreach (KeyValuePair<string, double> pair in sharesPerCompany)
                    {
                        cmd = new SqlCommand(@"SELECT TOP 1 current_price FROM company
                    WHERE ticker = @stockTick ORDER BY time_updated DESC", conn);
                        cmd.Parameters.AddWithValue("@stockTick", pair.Key);
                        user.TotalWorth += Convert.ToDecimal(cmd.ExecuteScalar()) * (decimal)pair.Value;
                    }
                }
                

            }
            return userList;
        }
        public List<Game> GetPendingGames(string username)
        {
            List<Game> games = new List<Game>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT game.id, game.name, organizer_id, start_date, end_date, balance, uORGANIZER.username FROM game
JOIN users_game ON game.id = users_game.game_id
JOIN users uPLAY ON uPLAY.id = users_game.users_id
JOIN users uORGANIZER ON game.organizer_id = uORGANIZER.id
WHERE uPLAY.username = @username AND users_game.status = 'pending'", conn);
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
        public bool AcceptInvitation(UserGame userGame, string username)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string QUERY = @"SELECT id FROM users WHERE username = @username";
                    SqlCommand cmd = new SqlCommand(QUERY, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    int userId = Convert.ToInt32(cmd.ExecuteScalar());
                    if (userId != userGame.UserId)
                    {
                        return false;
                    }

                    QUERY = @"UPDATE users_game
SET users_id = @userId, game_id = @gameId, status = 'approved', balance = @balance
WHERE users_id = @userId AND game_id = @gameId";
                    cmd = new SqlCommand(QUERY, conn);
                    cmd.Parameters.AddWithValue("@userId", userGame.UserId);
                    cmd.Parameters.AddWithValue("@gameId", userGame.GameId);
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
        public bool DeclineInvitation(UserGame userGame, string username)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string QUERY = @"SELECT id FROM users WHERE username = @username";
                    SqlCommand cmd = new SqlCommand(QUERY, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    int userId = Convert.ToInt32(cmd.ExecuteScalar());
                    if (userId != userGame.UserId)
                    {
                        return false;
                    }
                    QUERY = @"UPDATE users_game
SET users_id = @userId, game_id = @gameId, status = 'rejected'
WHERE users_id = @userId AND game_id = @gameId";
                    cmd = new SqlCommand(QUERY, conn);
                    cmd.Parameters.AddWithValue("@userId", userGame.UserId);
                    cmd.Parameters.AddWithValue("@gameId", userGame.GameId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
            return true;
        }
        public List<BuyModel> GetCurrentInvestments(string username, int gameId)
        {
            try
            {
                List<BuyModel> investments = new List<BuyModel>();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    const string QUERY = @"SELECT * FROM buy_table
JOIN users ON buy_table.users_id = users.id
JOIN company ON buy_table.stock_at_buy_id = company.stock_id
WHERE game_id = @gameId AND username = @username";
                    SqlCommand cmd = new SqlCommand(QUERY, conn);
                    cmd.Parameters.AddWithValue("@gameId", gameId);
                    cmd.Parameters.AddWithValue("@username", username);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        BuyModel investment = ReadToBuyModel(rdr);
                        investments.Add(investment);
                    }
                }
                return investments;
            }
            catch
            {
                throw;
            }
        }
        private BuyModel ReadToBuyModel(SqlDataReader rdr)
        {
            return new BuyModel
            {
                BuyId = Convert.ToInt32(rdr["id"]),
                UsersId = Convert.ToInt32(rdr["users_id"]),
                StockId = Convert.ToInt32(rdr["stock_at_buy_id"]),
                CompanyTicker = Convert.ToString(rdr["ticker"]),
                GameId = Convert.ToInt32(rdr["game_id"]),
                InitialSharesPurchased = Convert.ToDouble(rdr["initial_shares_purchased"]),
                SharesCurrentlyOwned = Convert.ToDouble(rdr["shares_currently_owned"]),
                AmountPerShare = Convert.ToDecimal(rdr["amount_per_share"]),
                BuyTimeTicks = Convert.ToInt64(rdr["time_purchased"]),
            };
        }
        private Game ReadToGame(SqlDataReader rdr)
        {
            Game game = new Game();
            game.GameId = Convert.ToInt32(rdr["id"]);
            game.Name = Convert.ToString(rdr["name"]);
            game.OrganizerName = Convert.ToString(rdr["username"]);
            game.OrganizerId = Convert.ToInt32(rdr["organizer_id"]);
            game.StartDate = Convert.ToDateTime(rdr["start_date"]);
            game.EndDate = Convert.ToDateTime(rdr["end_date"]);
            game.Balance = Convert.ToDecimal(rdr["balance"]);
            
            return game;
        }
        private UserInfo ReadToUserInfo(SqlDataReader rdr)
        {
            UserInfo user = new UserInfo();

            user.UserId = Convert.ToInt32(rdr["id"]);
            user.Username = Convert.ToString(rdr["username"]);
            user.Balance = Convert.ToDecimal(rdr["balance"]);
            return user;
        }
    }
}
