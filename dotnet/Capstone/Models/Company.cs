using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Company
    {
        public string Ticker { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal PreviousClosePrice { get; set; }
        public long Ticks { get; set; }
        public DateTime TimeLastUpdated { get; set; }

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
