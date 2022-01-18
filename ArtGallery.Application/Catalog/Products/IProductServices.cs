using ArtGallery.Data.Entities;
using ArtGallery.ViewModel.Catalog.Products;
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
        Task<Product> GetProduct(int productId);
        Task<bool> InsertProduct(InsertProductRequest Request);
        Task<bool> UpdateStatus(int id);
        Task<bool> UpdateProduct(EditProductRequest request);
        Task<bool> DeleteProduct(int productId);
        Task<List<Product>> ProductInCategory(string cateName);
    }
}
