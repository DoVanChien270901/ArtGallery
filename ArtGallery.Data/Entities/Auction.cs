using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Data.Entities
{
    public class Auction
    {
        public int Id { get; set; }
        public decimal StartingPrice { get; set; }
        public decimal PriceStep { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string AccountId { get; set; }
        public Account Account { get; set; }
        public List<AmountInAuction> AmountInAcctions { get; set; }
    }
}
