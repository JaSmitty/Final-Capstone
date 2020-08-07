using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class BuySellSqlDAO
    {
        private string connectionString;

        public BuySellSqlDAO(string dbconnectionString)
        {
            this.connectionString = dbconnectionString;
        }

        public BuyModel BuyStock(int userId, int gameId, int stockId, float numberOfShares)
        {
            Stock stock = new Stock();
            BuyModel buyModel = new BuyModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    //Get stock info
                    SqlCommand cmd = new SqlCommand("SELECT * FROM company where company.stock_id = @stockId", conn);
                    cmd.Parameters.AddWithValue("@stockId", stockId);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    rdr.Read();
                    stock = HelperStock(rdr);

                    //Store current ticks
                    long timeTicks = DateTime.Now.Ticks;

                    //Insert buy into database
                    SqlCommand cmd2 = new SqlCommand(@"INSERT into company(users_id, stock_at_buy_id, game_id, intitial_shares_purchased, shares_currently_owned, amount_per_share, time_purchased) 
                                                       VALUES (@userId, @stockBuyId, @gameId, @sharesPurchased, @currentlyOwned, @amountPerShare, @timePurchased); SELECT @@IDENTITY", conn);
                    cmd2.Parameters.AddWithValue("@userId", userId);
                    cmd2.Parameters.AddWithValue("@stockBuyId", stockId);
                    cmd2.Parameters.AddWithValue("@gameId", gameId);
                    cmd2.Parameters.AddWithValue("@sharesPurchased", numberOfShares);
                    cmd2.Parameters.AddWithValue("@currentlyOwned", numberOfShares);
                    cmd2.Parameters.AddWithValue("@amountPerShare", stock.C);
                    cmd2.Parameters.AddWithValue("@timePurchased", timeTicks);
                    buyModel.BuyId = Convert.ToInt32(cmd2.ExecuteScalar());
                    buyModel.AmountPerShare = stock.C;
                    buyModel.BuyTimeTicks = timeTicks;
                    buyModel.
                }
            }
            catch
            {
                throw;
            }
            return buyModel;
        }


        private Stock HelperStock(SqlDataReader rdr)
        {
            Stock newStock = new Stock();
            newStock.Ticker = Convert.ToString(rdr["ticker"]);
            newStock.CompanyName = Convert.ToString(rdr["company_name"]);
            newStock.O = Convert.ToDecimal(rdr["open_price"]);
            newStock.H = Convert.ToDecimal(rdr["high_price"]);
            newStock.L = Convert.ToDecimal(rdr["low_price"]);
            newStock.C = Convert.ToDecimal(rdr["current_price"]);
            newStock.PC = Convert.ToDecimal(rdr["previous_close_price"]);
            newStock.T = Convert.ToInt64(rdr["time_updated"]);
            return newStock;
        }
    }
}
