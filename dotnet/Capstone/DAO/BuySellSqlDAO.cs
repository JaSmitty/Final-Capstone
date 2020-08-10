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

        public int GetUserId(string username)
        {
            int returnUserId = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT id FROM users WHERE username = @username", conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    returnUserId = Convert.ToInt32(cmd.ExecuteScalar());

                }
            }
            catch (SqlException)
            {
                throw;
            }

            return returnUserId;
        }

        public BuyModel BuyStock(BuyModel buyModel) 
        {
            Stock stock = new Stock();
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
                    cmd.Parameters.AddWithValue("@stockId", buyModel.StockId);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    rdr.Read();
                    stock = HelperStock(rdr);
                    buyModel.AmountPerShare = stock.C;

                    //********************************************\\

                    //Store current ticks
                    long timeTicks = DateTime.Now.Ticks;
                    buyModel.BuyTimeTicks = timeTicks;

                    //Insert buy into database
                    SqlCommand cmd2 = new SqlCommand(@"INSERT into buy_table(users_id, stock_at_buy_id, game_id, initial_shares_purchased, shares_currently_owned, amount_per_share, time_purchased) 
                                                       VALUES (@userId, @stockBuyId, @gameId, @sharesPurchased, @currentlyOwned, @amountPerShare, @timePurchased); SELECT @@IDENTITY", conn);
                    cmd2.Parameters.AddWithValue("@userId", buyModel.UsersId);
                    cmd2.Parameters.AddWithValue("@stockBuyId", buyModel.StockId);
                    cmd2.Parameters.AddWithValue("@gameId", buyModel.GameId);
                    cmd2.Parameters.AddWithValue("@sharesPurchased", buyModel.InitialSharesPurchased);
                    cmd2.Parameters.AddWithValue("@currentlyOwned", buyModel.InitialSharesPurchased);
                    cmd2.Parameters.AddWithValue("@amountPerShare", stock.C);
                    cmd2.Parameters.AddWithValue("@timePurchased", timeTicks);
                    int id = Convert.ToInt32(cmd2.ExecuteScalar());
                    buyModel.BuyId = id;
                }
            }
            catch
            {
                throw;
            }
            return buyModel;
        }

        public SellModel SellStock(SellModel sellModel)
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
                          Select @@IDENTITY
                          Commit Transaction", conn);
                    cmd.Parameters.AddWithValue("@buyId", sellModel.BuyReferenceId);
                    cmd.Parameters.AddWithValue("@sharesSold", sellModel.SharesSold);
                    cmd.Parameters.AddWithValue("@stockAtSellId", sellModel.StockAtSellId);
                    cmd.Parameters.AddWithValue("@timeSold", timeTicks);
                    int id = Convert.ToInt32(cmd.ExecuteScalar());
                    sellModel.SellId = id;

                    //***********************************************************************************************\\

                    SqlCommand cmd2 = new SqlCommand("SELECT * FROM company where company.stock_id = @stockId", conn);
                    //cmd.Parameters.AddWithValue("@stockId", sellModel.StockId);
                    SqlDataReader rdr = cmd2.ExecuteReader();
                    rdr.Read();

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
