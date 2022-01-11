using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtGallery.Data.Entities;

namespace ArtGallery.ViewModel.Catalog.Products
{
    public class InsertProductRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }
        public int ViewCount { get; set; }
        public DateTime CreateDate { get; set; }
        public string ImagePath { get; set; }
        public string Caption { get; set; }
        public string Thumbnail { get; set; }
        public List<int> listCategoryId { get; set; } 
    }
}
