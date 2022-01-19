using ArtGallery.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.ViewModel.Catalog.Auctions
{
    public class CreateAuctionRequest
    {
        public int ProductId { get; set; }
        public string AccountId { get; set; }
        public decimal StartingPrice { get; set; }
        public decimal PriceStep { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}
