using Capstone.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore.InMemory.ValueGeneration.Internal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class StockSqlDAO : IStockDAO
    {
        private string connectionString;
        private BuySellSqlDAO buySell;
        public StockSqlDAO(string dbconnectionString)
        {
            this.connectionString = dbconnectionString;
        }

        public int AddStock(Stock stock)
        {
            int stockId = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT into company(ticker, open_price, high_price, low_price, current_price, previous_close_price, time_updated, company_name) VALUES (@ticker, @openPrice, @highPrice, @lowPrice, @currentPrice, @previousClosePrice, @timeUpdated, @companyName); SELECT @@IDENTITY", conn);
                    cmd.Parameters.AddWithValue("@ticker", stock.Ticker);
                    cmd.Parameters.AddWithValue("@openPrice", stock.O);
                    cmd.Parameters.AddWithValue("@highPrice", stock.H);
                    cmd.Parameters.AddWithValue("@lowPrice", stock.L);
                    cmd.Parameters.AddWithValue("@currentPrice", stock.C);
                    cmd.Parameters.AddWithValue("@previousClosePrice", stock.PC);
                    cmd.Parameters.AddWithValue("@timeUpdated", stock.T);
                    cmd.Parameters.AddWithValue("@companyName", stock.CompanyName);
                    stockId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch
            {
                throw;
            }
            return stockId;
        }

        public Stock GetCurrentStockByName(string stockTick)
        {
            Stock stock = new Stock();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM company where ticker = @stockTick ORDER BY time_updated DESC ", conn);
                    cmd.Parameters.AddWithValue("@stockTick", stockTick.ToUpper());
                    SqlDataReader rdr = cmd.ExecuteReader();
                    rdr.Read();
                    stock = HelperStock(rdr);
                }
            }
            catch
            {
                throw;
            }
            return stock;
        }
        public BuyModel GetInvestment(int stockId, int gameId, string username)
        {
            BuyModel buyModel = new BuyModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT buy_table.id AS buy_id, users_id, stock_at_buy_id, game_id, initial_shares_purchased, shares_currently_owned, amount_per_share, time_purchased FROM buy_table 
JOIN users ON users.id = buy_table.users_id
WHERE stock_at_buy_id = @stockId AND buy_table.game_id = @gameId AND users.username = @username", conn);
                    cmd.Parameters.AddWithValue("@stockId", stockId);
                    cmd.Parameters.AddWithValue("@gameId", gameId);
                    cmd.Parameters.AddWithValue("@username", username);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        buyModel = BuyModelHelper(rdr);
                    }
                }
            }
            catch
            {
                throw;
            }
            return buyModel;
        }
        public List<Stock> GetCurrentStocks(List<string> tickers)
        {
            try
            {
                List<Stock> stocks = new List<Stock>();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    foreach(string ticker in tickers)
                    {
                        Stock stock = new Stock();
                        const string QUERY = @"SELECT TOP 1 * FROM company
                                               WHERE ticker = @ticker
                                               ORDER BY time_updated DESC";
                        SqlCommand cmd = new SqlCommand(QUERY, conn);
                        cmd.Parameters.AddWithValue("@ticker", ticker);
                        SqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.Read())
                        {
                            stock = HelperStock(rdr);
                        }
                        rdr.Close();
                        stocks.Add(stock);
                    }
                }
                return stocks;
            }
            catch
            {
                throw;
            }
        }

        //************************************************************************
        //Game end

        public List<SellModel> GameEndSell(int gameId)
        {
            List<BuyModel> listOfStockBuys = new List<BuyModel>();
            List<SellModel> listOfStockSells = new List<SellModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"Select id, users_id, stock_at_buy_id, game_id, initial_shares_purchased, shares_currently_owned, amount_per_share, time_purchased, ticker
                                                        from buy_table
                                                        inner join company on company.stock_id = buy_table.stock_at_buy_id
                                                        where game_id = @gameIdToEnd", conn);
                    cmd.Parameters.AddWithValue("@gameIdToEnd", gameId);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        listOfStockBuys.Add(BuyModelHelper(rdr));
                    }
                    foreach (BuyModel stock in listOfStockBuys)
                    {
                        if (stock.SharesCurrentlyOwned > 0)
                        {
                            listOfStockSells.Add(SellStock(stock.BuyId, stock.SharesCurrentlyOwned, GetCurrentStockByName(stock.CompanyTicker).StockId));
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            return listOfStockSells;
        }

        private SellModel SellStock(int buyId, double sharesSold, int stockAtSellId)
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
                    cmd.Parameters.AddWithValue("@buyId", buyId);
                    cmd.Parameters.AddWithValue("@sharesSold", sharesSold);
                    cmd.Parameters.AddWithValue("@stockAtSellId", stockAtSellId);
                    cmd.Parameters.AddWithValue("@timeSold", timeTicks);
                    int id = Convert.ToInt32(cmd.ExecuteScalar());
                    sellObj.SellId = id;
                    //***********************************************************************************************\\

                    cmd = new SqlCommand("SELECT * FROM sell_table where id = @stockId", conn);
                    cmd.Parameters.AddWithValue("@stockId", sellObj.SellId);
                    SqlDataReader rdr = cmd.ExecuteReader();
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

        private Stock HelperStock(SqlDataReader rdr)
        {
            Stock newStock = new Stock();
            newStock.StockId = Convert.ToInt32(rdr["stock_id"]);
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
        private BuyModel BuyModelHelper(SqlDataReader rdr)
        {
            BuyModel newBuy = new BuyModel();
            newBuy.BuyId = Convert.ToInt32(rdr["id"]);
            newBuy.UsersId = Convert.ToInt32(rdr["users_id"]);
            newBuy.GameId = Convert.ToInt32(rdr["game_id"]);
            newBuy.StockId = Convert.ToInt32(rdr["stock_at_buy_id"]);
            newBuy.CompanyTicker = Convert.ToString(rdr["ticker"]);
            newBuy.InitialSharesPurchased = Convert.ToDouble(rdr["initial_shares_purchased"]);
            newBuy.SharesCurrentlyOwned = Convert.ToDouble(rdr["shares_currently_owned"]);
            newBuy.AmountPerShare = Convert.ToDecimal(rdr["amount_per_share"]);
            newBuy.BuyTimeTicks = Convert.ToInt64(rdr["time_purchased"]);
            return newBuy;
        }

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
    }
}
