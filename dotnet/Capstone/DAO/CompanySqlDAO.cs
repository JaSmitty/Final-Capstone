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
            Company company = new Company();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM company where ticker = @stockTick ORDER BY time_updated DESC ", conn);
                    cmd.Parameters.AddWithValue("@stockTick", stockTick.ToUpper());
                    SqlDataReader rdr = cmd.ExecuteReader();
                    rdr.Read();
                    company = HelperCompany(rdr);
                }
            }
            catch
            {

            }
            return company;
        }

        private Company HelperCompany(SqlDataReader rdr)
        {
            Company newCompany = new Company();
            newCompany.Ticker = Convert.ToString(rdr["ticker"]);
            newCompany.OpenPrice = Convert.ToDecimal(rdr["open_price"]);
            newCompany.HighPrice = Convert.ToDecimal(rdr["high_price"]);
            newCompany.LowPrice = Convert.ToDecimal(rdr["low_price"]);
            newCompany.CurrentPrice = Convert.ToDecimal(rdr["current_price"]);
            newCompany.PreviousClosePrice = Convert.ToDecimal(rdr["previous_close_price"]);
            newCompany.TimeLastUpdated = Convert.ToDateTime(rdr["time_updated"]);
            return newCompany;
        }
    }
}
