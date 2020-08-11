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
                    rdr.Close();
                    //********************************************\\

                    //Store current ticks
                    long timeTicks = DateTime.Now.Ticks;
                    buyModel.BuyTimeTicks = timeTicks;

                    //Insert buy into database
                    SqlCommand cmd2 = new SqlCommand(@"Begin Transaction
                                                        DECLARE @currentbalance money
                                                        select @currentbalance = balance
                                                        from users_game
                                                        where users_id = @userId and game_id = @gameId
                                                        UPDATE users_game
                                                        SET balance = (balance - (@sharesPurchased * @amountPerShare))
                                                        Where users_id = @userId and game_id = @gameId
                                                        INSERT into buy_table(users_id, stock_at_buy_id, game_id, initial_shares_purchased, shares_currently_owned, amount_per_share, time_purchased)
                                                        VALUES (@userId, @stockBuyId, @gameId, @sharesPurchased, @currentlyOwned, @amountPerShare, @timePurchased); 
                                                        SELECT @@IDENTITY
                                                        Commit Transaction", conn);
                    cmd2.Parameters.AddWithValue("@userId", buyModel.UsersId);
                    cmd2.Parameters.AddWithValue("@stockBuyId", buyModel.StockId);
                    cmd2.Parameters.AddWithValue("@gameId", buyModel.GameId);
                    cmd2.Parameters.AddWithValue("@sharesPurchased", buyModel.InitialSharesPurchased);
                    cmd2.Parameters.AddWithValue("@currentlyOwned", buyModel.InitialSharesPurchased);
                    cmd2.Parameters.AddWithValue("@amountPerShare", stock.C);
                    cmd2.Parameters.AddWithValue("@timePurchased", timeTicks);
                    int id = Convert.ToInt32(cmd2.ExecuteScalar());
                    buyModel.BuyId = id;
                    buyModel.SharesCurrentlyOwned = buyModel.InitialSharesPurchased;
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
                          DECLARE  @userId int
                          DECLARE  @gameId int

                          Select @shares_owned = shares_currently_owned, @amountPerShareBuy = amount_per_share, @userId = users_id, @gameId = game_id
                          From buy_table
                          Where id = @buyId

                          UPDATE buy_table
                          SET shares_currently_owned = (@shares_owned - @sharesSold)
                          Where id = @buyId

                          Select @amountPerShareSold = company.current_price
                          From company
                          where stock_id = @stockAtSellId

                          DECLARE @currentbalance money
                          select @currentbalance = balance
                          from users_game
                          where users_id = @userId and game_id = @gameId

                          UPDATE users_game
                          SET balance = (balance + (@sharesSold * @amountPerShareSold))
                          Where users_id = @userId and game_id = @gameId

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
                    cmd.Parameters.AddWithValue("@stockId", sellModel.SellId);
                    SqlDataReader rdr = cmd2.ExecuteReader();
                    rdr.Read();
                    sellObj = SellModelHelper(rdr);

                }
            }
            catch
            {
                throw;
            }
            return sellObj;
        }


        //public List<BuyModel> GetAllPastBuys(int userId)
        //{
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();
        //            SqlCommand cmd = new SqlCommand(@"Select *
        //                                              from buy_table
        //                                              where users_id = 1", conn);
        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

            private SellModel SellModelHelper(SqlDataReader rdr)
        {
            SellModel newSell = new SellModel();
            newSell.SellId = Convert.ToInt32(rdr["id"]);
            newSell.StockAtSellId = Convert.ToInt32(rdr["stock_at_sell_id"]);
            newSell.BuyReferenceId = Convert.ToInt32(rdr["buy_reference_id"]);
            newSell.SharesSold = Convert.ToDouble(rdr["id"]);
            newSell.PricePerShare = Convert.ToInt32(rdr["id"]);
            newSell.Profit = Convert.ToDecimal(rdr["profit"]);
            newSell.SellTimeTicks = Convert.ToInt64(rdr["time_sold"]);
            return newSell;
        }
        


        private Stock HelperStock(SqlDataReader rdr)
        {
            Stock newStock = new Stock();
            
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
