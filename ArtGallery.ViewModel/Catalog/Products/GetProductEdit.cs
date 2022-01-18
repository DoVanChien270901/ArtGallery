using ArtGallery.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.ViewModel.Catalog.Products
{
    public class GetProductEdit
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<ProductImage> Images { get; set; }
        public List<SelectListCate> SelectListCates { get; set; }

    }
}
