using ArtGallery.Data.EF;
using ArtGallery.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Application.Catalog.Products
{
    public class ProductImageServicesImp : IProductImageServices
    {
        private readonly ArtGalleryDbContext context;
        public ProductImageServicesImp(ArtGalleryDbContext context)
        {
            this.context = context;
        }

        // Get Product Image
        public async Task<ProductImage> GetProductImage(int Id)
        {
            return context.ProductImages.SingleOrDefault(c => c.Id.Equals(Id));
        }

        // Get Product Images
        public async Task<IEnumerable<ProductImage>> GetProductImages()
        {
            return context.ProductImages.ToList();
        }
    }
}
