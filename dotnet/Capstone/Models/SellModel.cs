using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class SellModel
    {
        public int SellId { get; set; }
        public int StockAtSellId { get; set; }
        public int BuyReferenceId { get; set; }
        public double SharesSold { get; set; }
        public decimal PricePerShare { get; set; }
        public decimal Profit { get; set; }
        public long SellTimeTicks { get; set; }
        public DateTime SellTime 
        {
            get
            {
                DateTime date = new DateTime(this.SellTimeTicks);
                return date;
            }
        }
    }
}
