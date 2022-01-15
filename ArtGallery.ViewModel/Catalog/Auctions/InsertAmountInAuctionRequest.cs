using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.ViewModel.Catalog.Auctions
{
    public class InsertAmountInAuctionRequest
    {
        public decimal LowestPrice { get; set; }
        public decimal NewPrice { get; set; }
        public int AuctionId { get; set; }
        public string AccountId { get; set; }
    }
}
