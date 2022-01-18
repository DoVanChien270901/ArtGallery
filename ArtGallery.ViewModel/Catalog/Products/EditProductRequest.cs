using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtGallery.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace ArtGallery.ViewModel.Catalog.Products
{
    public class EditProductRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IFormFile Thumbnail { get; set; }
        public List<IFormFile> Images { get; set; }
        public List<int> ListCategoryId { get; set; }
        public List<SelectListCate> Selecteds { get; set; }
    }
}
