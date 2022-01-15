using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtGallery.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace ArtGallery.ViewModel.Catalog.ProductImages
{
    public class InsertProductImageReq
    {
        public string Caption { get; set; }
        public int ProductId { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
