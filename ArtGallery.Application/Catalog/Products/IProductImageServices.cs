using ArtGallery.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Application.Catalog.Products
{
    public interface IProductImageServices
    {
        Task<IEnumerable<ProductImage>> GetProductImages();
        Task<ProductImage> GetProductImage(int Id);
        //Task<bool> InsertProductImage(ProductImage productImage);
        //Task<bool> UpdateProductImage(ProductImage Id);
        //Task<bool> DeleteProductImage(int Id);
    }
}
