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
    public class ProductsManagerController : ControllerBase
    {
        private readonly IProductServices productServices;
        public ProductsManagerController(IProductServices productServices)
        {
            this.productServices = productServices;
        }

        [HttpDelete("{Id:int}")]
        public async Task<bool> DeleteProduct(int Id)
        {
            return await productServices.DeleteProduct(Id);
        }
       
        [HttpGet]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await productServices.GetProducts();
        }

        [HttpGet("{title}")]
        public async Task<IEnumerable<Product>> SearchProduct(string title)
        {
            return await productServices.SearchProduct(title);
        }

        [HttpGet("{Id:int}")]
        public async Task<Product> GetProduct(int Id)
        {
            return await productServices.GetProduct(Id);
        }

        [HttpPut]
        public async Task<bool> UpdateProductForAdmin(Product Id)
        {
            return await productServices.UpdateProductForAdmin(Id);
        }

        [HttpGet("ProIncate/{cateName}")]
        public List<Product> ProductInCategory(string cateName)
        {
            return productServices.ProductInCategory(cateName);
        }

        [HttpPost("InsertProduct")]
        public Task<bool> InsertProduct(InsertProductRequest request)
        {
            return productServices.InsertProduct(request);
        }
    }
}
