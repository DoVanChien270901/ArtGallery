using ArtGallery.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.ViewModel.Catalog.Products
{
    public class ProductModelView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreateDate { get; set; }
        //public int CateId { get; set; }
        public bool Status { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public Product Product { get; set; }
        public Category Category { get; set; }
        public List<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
    }
}
