using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Investment
    {
        public int InvestmentId { get; set; }
        public int UserId { get; set; }
        public string Ticker { get; set; }
        public int GameId { get; set; }
        public double Shares { get; set; }
        public decimal Amount { get; set; }

        //public Investment(int user_id, string ticker, int game_id, double shares, decimal amount)
        //{
        //    UserId = user_id;
        //    Ticker = ticker;
        //    GameId = game_id;
        //    Shares = shares;
        //    Amount = amount;
        //}

    }
}
