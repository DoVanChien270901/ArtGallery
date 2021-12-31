using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Data.Entities
{
    public class AmountAuction
    {
        public int Id { get; set; }
        public decimal NewPrice { get; set; }
        public int AuctionId { get; set; }
        public Auction Auction { get; set; }
        public int ProfileUserId { get; set; }
        public ProfileUser ProfileUser { get; set; }
    }
}
