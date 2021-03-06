﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Capstone.Models;
using RestSharp;
using RestSharp.Authenticators;

namespace Capstone.API
{
    
    public class StockAPI
    {
        //string API_URL = "https://finnhub.io/api/v1/quote?symbol=AAPL&token=bskkcjvrh5rdcdh7faa0";
        // Josh Key = bspa7n7rh5r8ktikdj50
        // Jason Key = bskkcjvrh5rdcdh7faa0
        public Stock GetCompanyStockInfo(string ticker, string companyName)
        {
            string API_URL = "https://finnhub.io/api/v1";
            RestClient client = new RestClient(API_URL);


            RestRequest request = new RestRequest($"quote?symbol={ticker}&token=bskkcjvrh5rdcdh7faa0", DataFormat.Json);


            IRestResponse<Stock> stockResponse = client.Get<Stock>(request);
            CheckResponse(stockResponse);
            stockResponse.Data.Ticker = ticker;
            stockResponse.Data.CompanyName = companyName;
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
