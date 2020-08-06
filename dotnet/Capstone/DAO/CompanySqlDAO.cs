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
                    SqlCommand cmd = new SqlCommand("INSERT into company(ticker, open_price, high_price, low_price, current_price, previous_close_price, time_updated, company_name) VALUES (@ticker, @openPrice, @highPrice, @lowPrice, @currentPrice, @previousClosePrice, @timeUpdated, @companyName); SELECT @@IDENTITY", conn);
                    cmd.Parameters.AddWithValue("@ticker", company.Ticker);
                    cmd.Parameters.AddWithValue("@openPrice", company.O);
                    cmd.Parameters.AddWithValue("@highPrice", company.H);
                    cmd.Parameters.AddWithValue("@lowPrice", company.L);
                    cmd.Parameters.AddWithValue("@currentPrice", company.C);
                    cmd.Parameters.AddWithValue("@previousClosePrice", company.PC);
                    cmd.Parameters.AddWithValue("@timeUpdated", company.T);
                    cmd.Parameters.AddWithValue("@companyName", company.CompanyName);
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
            newCompany.O = Convert.ToDecimal(rdr["open_price"]);
            newCompany.H = Convert.ToDecimal(rdr["high_price"]);
            newCompany.L = Convert.ToDecimal(rdr["low_price"]);
            newCompany.C = Convert.ToDecimal(rdr["current_price"]);
            newCompany.PC = Convert.ToDecimal(rdr["previous_close_price"]);
            newCompany.T = Convert.ToInt64(rdr["time_updated"]);
            return newCompany;
        }
    }
}
