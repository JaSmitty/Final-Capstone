using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class BuyModel
    {
        public BuyModel()
        {
        }

        public BuyModel(int buyId, int usersId, int gameId, int stockId, float sharesPurchaed, float sharesOwned, decimal amountPerShare, long buyTick)
        {
            this.BuyId = buyId;
            this.UsersId = usersId;
            this.GameId = gameId;
            this.StockId = stockId;
            this.InitialSharesPurchased = sharesPurchaed;
            this.SharesCurrentlyOwned = sharesOwned;
            this.AmountPerShare = amountPerShare;
            this.BuyTimeTicks = buyTick;
        }


        public int BuyId { get; set; }
        public int UsersId { get; set; }
        public int GameId { get; set; }
        public int StockId { get; set; }
        public string CompanyTicker { get; set; }
        public double InitialSharesPurchased { get; set; }
        public double SharesCurrentlyOwned { get; set; }
        public decimal AmountPerShare { get; set; }
        public long BuyTimeTicks { get; set; }
        
        public DateTime BuyTime
        {
            get
            {
                DateTime date = new DateTime(this.BuyTimeTicks);
                return date;
            }
        }
    }
}
