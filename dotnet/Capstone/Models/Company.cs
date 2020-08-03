using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Company
    {
        public string Ticker { get; set; }
        public decimal Open_Price { get; set; }
        public decimal High_Price { get; set; }
        public decimal Low_Price { get; set; }
        public decimal Current_Price { get; set; }
        public decimal Previous_Close_Price { get; set; }

        public Company(string ticker, decimal open_price, decimal high_price, decimal low_price, decimal current_price, decimal previous)
        {
            Ticker = ticker;
            Open_Price = open_price;
            High_Price = high_price;
            Low_Price = low_price;
            Current_Price = current_price;
            Previous_Close_Price = previous;
        }
    }
}
