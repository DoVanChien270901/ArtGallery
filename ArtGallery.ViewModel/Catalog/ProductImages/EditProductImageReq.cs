using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtGallery.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace ArtGallery.ViewModel.Catalog.ProductImages
{
    public class EditProductImageReq
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
