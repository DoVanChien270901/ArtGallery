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
        //public List<Category> listcate();
        public decimal CurrentPrice { get; set; }
        public decimal PriceStep { get; set; }
        public decimal NewPrice { get; set; }
        public int AuctionId { get; set; }
        public int ProfileUserId { get; set; }
    }
}
