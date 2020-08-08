using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Stock
    {
        public int StockId { get; set; }
        public string CompanyName { get; set; }


        public string Ticker { get; set; }


        //Open Price
        public decimal O { get; set; }


        //High of the day
        public decimal H { get; set; }


        //Low of the day
        public decimal L { get; set; }


        //Current Price
        public decimal C { get; set; }


        //Previous Close Price
        public decimal PC { get; set; }


        //Time Ticks
        public long T { get; set; }


        public DateTime TimeLastUpdated 
        {
            get
            {
                return UnixTimeStampToDateTime(this.T).AddHours((double)-4);
                
            }
        }

        //private DateTime HelperDatetime(long ticks)
        //{
        //    DateTime date = new DateTime(ticks);
        //    return date;
        //}

        private  DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            System.DateTime dtDateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);
            return dtDateTime;
        }

        //public Company(string ticker, decimal open_price, decimal high_price, decimal low_price, decimal current_price, decimal previous)
        //{
        //    Ticker = ticker;
        //    OpenPrice = open_price;
        //    HighPrice = high_price;
        //    LowPrice = low_price;
        //    CurrentPrice = current_price;
        //    PreviousClosePrice = previous;
        //}
    }
}
