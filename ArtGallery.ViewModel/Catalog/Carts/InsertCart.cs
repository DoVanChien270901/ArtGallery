using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.ViewModel.Catalog.Carts
{
    public class InsertCart
    {
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
        public decimal Commision { get; set; }
        public string Description { get; set; }
        public string AccountId { get; set; }
    }
}
