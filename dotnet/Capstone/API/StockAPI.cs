using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;
using RestSharp;
using RestSharp.Authenticators;

namespace Capstone.API
{
    
    public class StockAPI
    {
        //string API_URL = "https://finnhub.io/api/v1/quote?symbol=AAPL&token=bskkcjvrh5rdcdh7faa0";

        public Company GetCompanyStockInfo(string ticker)
        {
            string API_URL = "https://finnhub.io/api/v1";
            RestClient client = new RestClient(API_URL);

            RestRequest request = new RestRequest($"quote?symbol={ticker}&token=bskkcjvrh5rdcdh7faa0", DataFormat.Json);

            IRestResponse<Company> stockResponse = client.Get<Company>(request);
            CheckResponse(stockResponse);
            return stockResponse.Data;
        }

        private static void CheckResponse(IRestResponse response)
        {
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.");
            }

            if (!response.IsSuccessful)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception("Authorization is required for this option. Please log in.");
                }
                if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    throw new Exception("You do not have permission to perform the requested action");
                }

                throw new Exception($"Error occurred - received non-success response: {response.StatusCode} ({(int)response.StatusCode})");
            }
        }
    }
}
