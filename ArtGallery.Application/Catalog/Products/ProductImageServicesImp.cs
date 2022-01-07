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

        //public async Task<bool> DeleteProductImage(int Id)
        //{
        //    var model = context.ProductImages.SingleOrDefault(c => c.Id.Equals(Id));
        //    if (model != null)
        //    {
        //        context.ProductImages.Remove(model);
        //        await context.SaveChangesAsync();
        //        return true;
        //    }
        //    return false;
        //}

        public async Task<ProductImage> GetProductImage(int Id)
        {
            return context.ProductImages.SingleOrDefault(c => c.Id.Equals(Id));
        }

        public async Task<IEnumerable<ProductImage>> GetProductImages()
        {
            return context.ProductImages.ToList();
        }

        //public async Task<bool> InsertProductImage(ProductImage productImage)
        //{
        //    var model = context.ProductImages.SingleOrDefault(c => c.Id.Equals(productImage.Id));
        //    if (model == null)
        //    {
        //        await context.ProductImages.AddAsync(model);
        //        await context.SaveChangesAsync();
        //        return true;
        //    }
        //    return false;
        //}

        //public async Task<bool> UpdateProductImage(ProductImage Id)
        //{
        //    var model = context.ProductImages.SingleOrDefault(c => c.Id.Equals(Id.Id));
        //    if (model != null)
        //    {
        //        model.Caption = Id.Caption;
        //        model.ImagePath = Id.ImagePath;
        //        model.Thumbnail = Id.Thumbnail;
        //        context.SaveChanges();
        //        return true;
        //    }
        //    return false;
        //}
    }
}
