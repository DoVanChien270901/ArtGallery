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
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<Product>> SearchProduct(string title);
        Task<Product> GetProduct(int Id);
        Task<bool> InsertProduct(InsertProductRequest request);
        Task<bool> UpdateProductForAdmin(Product Id);
        Task<bool> DeleteProduct(int Id);
        List<Product> ProductInCategory(string cateName);
    }
}
