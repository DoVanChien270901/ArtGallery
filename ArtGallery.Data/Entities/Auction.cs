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
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public List<AmountAuction> AmountAcctions { get; set; }
    }
}
