using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ViewCount { get; set; }
        public decimal Price { get; set; }
        public DateTime CreateDate { get; set; }
        public Category Category { get; set; }
        public Auction Auction { get; set; }
        public List<ProductInCart> ProductInCarts { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<Order> Orders { get; set; }
    }
}
