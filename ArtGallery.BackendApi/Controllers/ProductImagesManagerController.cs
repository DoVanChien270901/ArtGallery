using ArtGallery.Application.Catalog.Products;
using ArtGallery.Data.Entities;
using ArtGallery.ViewModel.Catalog.Products;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtGallery.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImagesManagerController : ControllerBase
    {
        private readonly IProductImageServices productImageServices;
        public ProductImagesManagerController(IProductImageServices productImageServices)
        {
            this.productImageServices = productImageServices;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductImage>> GetProductImages()
        {
            return await productImageServices.GetProductImages();
        }

        [HttpGet("{Id:int}")]
        public async Task<ProductImage> GetProductImage(int Id)
        {
            return await productImageServices.GetProductImage(Id);
        }

    }
}
