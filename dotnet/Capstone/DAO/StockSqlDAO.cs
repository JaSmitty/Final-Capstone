using Capstone.Models;
using Microsoft.AspNetCore.Authentication;
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
                        buyModel = ReadToBuyModel(rdr);
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

        private BuyModel ReadToBuyModel(SqlDataReader rdr)
        {
            return new BuyModel
            {
                BuyId = Convert.ToInt32(rdr["buy_id"]),
                StockId = Convert.ToInt32(rdr["stock_at_buy_id"]),
                UsersId = Convert.ToInt32(rdr["users_id"]),
                GameId = Convert.ToInt32(rdr["game_id"]),
                InitialSharesPurchased = Convert.ToDouble(rdr["initial_shares_purchased"]),
                SharesCurrentlyOwned = Convert.ToDouble(rdr["shares_currently_owned"]),
                AmountPerShare = Convert.ToDecimal(rdr["amount_per_share"]),
                BuyTimeTicks = Convert.ToInt64(rdr["time_purchased"])
            };
        }
    }
}
