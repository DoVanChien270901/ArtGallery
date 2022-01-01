using ArtGallery.Data.EF;
using ArtGallery.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Application.Catalog.Products
{
    public class ProductServicesImp : IProductServices
    {
        private readonly ArtGalleryDbContext context;
        public ProductServicesImp(ArtGalleryDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> DeleteProduct(int Id)
        {
            var model = context.Products.SingleOrDefault(c => c.Id.Equals(Id));
            if (model != null)
            {
                context.Products.Remove(model);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Product> GetProduct(int Id)
        {
            return context.Products.SingleOrDefault(c => c.Id.Equals(Id));
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return context.Products.ToList();
        }

        public async Task<bool> InsertProduct(Product product)
        {
            var model = context.Products.SingleOrDefault(c => c.Id.Equals(product.Id));
            if (model == null)
            {
                model.Status = false;
                await context.Products.AddAsync(model);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Product>> SearchProduct(string title)
        {
            return context.Products.Where(c => c.Title.Contains(title));
        }

        public async Task<bool> UpdateProduct(Product Id)
        {
            var model = context.Products.SingleOrDefault(c => c.Id.Equals(Id.Id));
            if (model != null)
            {
                model.Title = Id.Title;
                model.Description = Id.Description;
                model.Price = Id.Price;
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
