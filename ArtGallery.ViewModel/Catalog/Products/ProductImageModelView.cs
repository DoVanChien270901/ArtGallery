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
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
