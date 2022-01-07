using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Data.Entities
{
    public class AmountInAuction
    {
        public int Id { get; set; }
        public decimal NewPrice { get; set; }
        public int AuctionId { get; set; }
        public Auction Auction { get; set; }
        public string AccountId { get; set; }
        public Account Account { get; set; }
    }
}
