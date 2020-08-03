using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Investment
    {
        public int Investment_ID { get; set; }
        public int User_ID { get; set; }
        public string Ticker { get; set; }
        public int Game_ID { get; set; }
        public double Shares { get; set; }
        public decimal Amount { get; set; }

        public Investment(int user_id, string ticker, int game_id, double shares, decimal amount)
        {
            User_ID = user_id;
            Ticker = ticker;
            Game_ID = game_id;
            Shares = shares;
            Amount = amount;
        }

    }
}
