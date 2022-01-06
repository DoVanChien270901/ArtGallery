using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtGallery.Data.Entities;

namespace ArtGallery.ViewModel.Catalog.Products
{
    public class ProductImageModelView
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string Caption { get; set; }
        public string Thumbnail { get; set; }
        public int ProductId { get; set; }
        public IEnumerable<ProductImage> ProductImages { get; set; }
        public ProductImage ProductImage { get; set; }
        //public List<Product> Product { get; set; }
    }
}
