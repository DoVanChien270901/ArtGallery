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
        private readonly IProductServices _managerProduct;
        public ProductsManagerController(IProductServices _managerProduct)
        {
            this._managerProduct = _managerProduct;
        }

        [HttpDelete("{productId:int}")]
        public async Task<bool> DeleteProduct([FromForm] int productId)
        {
            return await _managerProduct.DeleteProduct(productId);
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _managerProduct.GetProducts();
        }

        [HttpGet("{title}")]
        public async Task<IEnumerable<Product>> SearchProduct(string title)
        {
            return await _managerProduct.SearchProduct(title);
        }

        [HttpGet("{productId:int}")]
        public async Task<Product> GetProduct(int productId)
        {
            return await _managerProduct.GetProduct(productId);
        }

        [HttpGet("ProductInCategory/{cateName}")]
        public List<Product> ProductInCategory(string cateName)
        {
            return _managerProduct.ProductInCategory(cateName);
        }

        [HttpPost("InsertProduct")]
        public async Task<bool> InsertProduct(InsertProductRequest request)
        {
            return await _managerProduct.InsertProduct(request);
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