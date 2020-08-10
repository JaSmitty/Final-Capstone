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
            BuyModel buyModel;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    //*******************************************
                    //May have to change into a transaction later
                    //*******************************************
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
                    SqlCommand cmd2 = new SqlCommand(@"INSERT into company(users_id, stock_at_buy_id, game_id, initial_shares_purchased, shares_currently_owned, amount_per_share, time_purchased) 
                                                       VALUES (@userId, @stockBuyId, @gameId, @sharesPurchased, @currentlyOwned, @amountPerShare, @timePurchased); SELECT @@IDENTITY", conn);
                    cmd2.Parameters.AddWithValue("@userId", userId);
                    cmd2.Parameters.AddWithValue("@stockBuyId", stockId);
                    cmd2.Parameters.AddWithValue("@gameId", gameId);
                    cmd2.Parameters.AddWithValue("@sharesPurchased", numberOfShares);
                    cmd2.Parameters.AddWithValue("@currentlyOwned", numberOfShares);
                    cmd2.Parameters.AddWithValue("@amountPerShare", stock.C);
                    cmd2.Parameters.AddWithValue("@timePurchased", timeTicks);
                    int id = Convert.ToInt32(cmd2.ExecuteScalar());
                    buyModel = new BuyModel(id, userId, gameId, stockId, numberOfShares, numberOfShares, stock.C, timeTicks);
                }
            }
            catch
            {
                throw;
            }
            return buyModel;
        }

        public SellModel SellStock(int buyStockId, int stockAtSellId, float numberOfShares)
        {
            SellModel sellObj = new SellModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    //Store current ticks
                    long timeTicks = DateTime.Now.Ticks;

                    //Insert buy into database
                    SqlCommand cmd = new SqlCommand(
                        @"BEGIN TRANSACTION

                          DECLARE  @shares_owned float
                          DECLARE  @amountPerShareBuy money
                          DECLARE  @amountPerShareSold money

                          Select @shares_owned = shares_currently_owned, @amountPerShareBuy = amount_per_share
                          From buy_table
                          Where id = @buyId

                          UPDATE buy_table
                          SET shares_currently_owned = (@shares_owned - @sharesSold)
                          Where id = @buyId

                          Select @amountPerShareSold = company.current_price
                          From company
                          where stock_id = @stockAtSellId

                          INSERT INTO sell_table(stock_at_sell_id, buy_reference_id, shares_sold, amount_per_share, profit, time_sold)
                          VALUES(@stockAtSellId, @buyId, @sharesSold, @amountPerShareSold, ((@amountPerShareSold * @sharesSold) - (@amountPerShareBuy * @sharesSold)), @timeSold)
                          Commit Transaction", conn);
                    cmd.Parameters.AddWithValue("@buyId", buyStockId);
                    cmd.Parameters.AddWithValue("@sharesSold", numberOfShares);
                    cmd.Parameters.AddWithValue("@stockAtSellId", stockAtSellId);
                    cmd.Parameters.AddWithValue("@timeSold", timeTicks);

                }
            }
            catch
            {
                throw;
            }
            return sellObj;
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
