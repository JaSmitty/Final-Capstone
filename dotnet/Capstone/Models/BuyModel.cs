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

        public BuyModel(int buyId, string userId, int gameId, int stockId, float sharesPurchaed, float sharesOwned, decimal amountPerShare, long buyTick)
        {
            this.BuyId = buyId;
            this.UserId = userId;
            this.GameId = gameId;
            this.StockId = stockId;
            this.InitialSharesPurchased = sharesPurchaed;
            this.SharesCurrentlyOwned = sharesOwned;
            this.AmountPerShare = amountPerShare;
            this.BuyTimeTicks = buyTick;
        }


        public int BuyId { get; set; }
        public string UserId { get; set; }
        public int GameId { get; set; }
        public int StockId { get; set; }
        public float InitialSharesPurchased { get; set; }
        public float SharesCurrentlyOwned { get; set; }
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
