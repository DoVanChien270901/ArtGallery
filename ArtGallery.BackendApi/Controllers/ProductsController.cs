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
    public class ProductsController : ControllerBase
    {
        private readonly IProductServices _managerProduct;
        public ProductsController(IProductServices managerProduct)
        {
            _managerProduct = managerProduct;
        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<bool> DeleteProduct(int id)
        {
            return await _managerProduct.DeleteProduct(id);
        }

        [HttpGet("AllProduct")]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _managerProduct.GetProducts();
        }
        [HttpGet("GetProduct/{id}")]
        public async Task<Product> GetProduct(int id)
        {
            return await _managerProduct.GetProduct(id);
        }

        [HttpGet("ProductInCategory/{cateName}")]
        public Task<List<Product>> ProductInCategory(string cateName)
        {
            return _managerProduct.ProductInCategory(cateName);
        }

        [HttpPost("InsertProduct")]
        [Consumes("multipart/form-data")]
        public async Task<bool> InsertProduct([FromForm] InsertProductRequest request)
        {
            await _managerProduct.InsertProduct(request);
            return true;
        }

        [HttpPut("UpdateProduct")]
        [Consumes("multipart/form-data")]
        public async Task<bool> UpdateProduct([FromForm] EditProductRequest request)
        {
            return await _managerProduct.UpdateProduct(request);
        }

        [HttpGet("UpdateStatus/{id}")]
        public async Task<bool> UpdateStatus(int id)
        {
            return await _managerProduct.UpdateStatus(id);
        }
    }
}