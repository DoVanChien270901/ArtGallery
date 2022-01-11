using ArtGallery.Data.Entities;
using ArtGallery.ViewModel.Catalog.Products;
using ArtGallery.ViewModel.Catalog.ProductImages;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Application.Catalog.Products
{
    public interface IProductServices
    {
        // Product
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<Product>> SearchProduct(string title);
        Task<Product> GetProduct(int productId);
        Task<bool> InsertProduct(InsertProductRequest Request);
        Task<bool> UpdateStatus(Product productId);
        Task<bool> UpdateProduct(EditProductRequest request);
        Task<bool> DeleteProduct(int productId);
        List<Product> ProductInCategory(string cateName);
    }
}
