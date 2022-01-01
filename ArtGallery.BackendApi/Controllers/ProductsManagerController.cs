using ArtGallery.Application.System.Admin;
using ArtGallery.Application.Catalog.Products;
using ArtGallery.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpPost]
        public async Task<bool> InsertProduct(Product product)
        {
            return await productServices.InsertProduct(product);
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
        public async Task<bool> UpdateProduct(Product Id)
        {
            return await productServices.UpdateProduct(Id);
        }
    }
}
