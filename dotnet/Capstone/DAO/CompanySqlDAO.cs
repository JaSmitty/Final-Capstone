using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class CompanySqlDAO : ICompanyDAO
    {
        private string connectionString;

        public CompanySqlDAO(string dbconnectionString)
        {
            this.connectionString = dbconnectionString;
        }

        public int AddStock(Company company)
        {
            int stockId = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT into company(ticker, open_price, high_price, low_price, current_price, previous_close_price, time_updated) VALUES (@ticker, @openPrice, @highPrice, @lowPrice, @currentPrice, @previousClosePrice, @timeUpdated); SELECT @@IDENTITY", conn);
                    cmd.Parameters.AddWithValue("@ticker", company.Ticker);
                    cmd.Parameters.AddWithValue("@openPrice", company.OpenPrice);
                    cmd.Parameters.AddWithValue("@highPrice", company.HighPrice);
                    cmd.Parameters.AddWithValue("@lowPrice", company.LowPrice);
                    cmd.Parameters.AddWithValue("@currentPrice", company.CurrentPrice);
                    cmd.Parameters.AddWithValue("@previousClosePrice", company.PreviousClosePrice);
                    cmd.Parameters.AddWithValue("@timeUpdated", DateTime.Now);
                    stockId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch
            {

            }
            return stockId;
        }

        public Company GetCurrentStockByName(string stockTick)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * from company where company_ticker = @stockTick && ", conn);
                    
                }
            }
            catch
            {

            }
            Company company = new Company();
            return company;
        }
    }
}
