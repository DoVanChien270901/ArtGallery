using ArtGallery.Application.Catalog.Products;
using ArtGallery.Data.Entities;
using ArtGallery.ViewModel.Catalog.Products;
using ArtGallery.ViewModel.Catalog.ProductImages;
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
        [HttpGet]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _managerProduct.GetProducts();
        }

        [HttpGet("SearchProduct/{title}")]
        public async Task<IEnumerable<Product>> SearchProduct(string title)
        {
            return await _managerProduct.SearchProduct(title);
        }

        [HttpGet("GetProduct/{id}")]
        public async Task<Product> GetProduct( int id)
        {
            return await _managerProduct.GetProduct(id);
        }

        [HttpGet("ProductInCategory/{cateName}")]
        public Task<List<Product>> ProductInCategory( string cateName)
        {
            return _managerProduct.ProductInCategory(cateName);
        }

        [HttpPost("InsertProduct")]
        public async Task<bool> InsertProduct([FromForm] InsertProductRequest request)
        {
            await _managerProduct.InsertProduct(request);
            return true;
        }

        [HttpPut("UpdateProduct")]
        public async Task<bool> UpdateProduct(EditProductRequest request)
        {
            return await _managerProduct.UpdateProduct(request);
        }

        [HttpPut("UpdateStatus")]
        public async Task<bool> UpdateStatus([FromForm] Product productId)
        {
            return await _managerProduct.UpdateStatus(productId);
        }
    }
}
