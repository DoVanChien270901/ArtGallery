using ArtGallery.Data.EF;
using ArtGallery.Data.Entities;
using ArtGallery.ViewModel.Catalog.ProductImages;
using ArtGallery.ViewModel.Catalog.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
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

        // Product In Category
        public List<Product> ProductInCategory(string cateName)
        {
            if(cateName != null)
            {
                //Category cateId = context.Categories.SingleOrDefault(c=>c.Name == cateName);
                //List<ProductInCategory> productId = context.ProductInCategories.Where(c => c.CategoryId.Equals(cateId.Id)).ToList();
                //List<Product> listProduct = null;
                //foreach (var item in productId)
                //{
                //    int i = 0;
                //    Product product = context.Products.SingleOrDefault(c => c.Id.Equals(item.ProductId));
                //    listProduct.Insert(i, product);
                //    i++;
                //};
                ////Category category = context.Categories.SingleOrDefault(c => c.Name.Equals(cateName));
                ////return context.Products.Where(c => c.CategoryId.Equals(category.Id)).ToList();
                var ListProduct = 
                    from p in context.Products
                    join pic in context.ProductInCategories on p.Id equals pic.ProductId
                    join c in context.Categories on pic.CategoryId equals c.Id
                    where c.Name.Equals(cateName)
                    select new Product {
                        Id = p.Id,
                        Title = p.Title,
                        Description = p.Description,
                        Price = p.Price,
                        Status = p.Status,
                        ViewCount = p.ViewCount,
                        CreateDate = p.CreateDate
                    };                    
              
                return ListProduct.ToList();
            }
            return null;     
        }

        // Delete Product
        public async Task<bool> DeleteProduct(int productId)
        {
            var model = context.Products.SingleOrDefault(c => c.Id.Equals(productId));
            if (model != null)
            {
                //var images = context.ProductImages.Where(c => c.ProductId == model.Id);
                context.Products.Remove(model);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Product> GetProduct(int productId)
        {
            return context.Products.SingleOrDefault(c => c.Id.Equals(productId));
        }

        // Get List
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return context.Products.ToList();
        }

        // Insert Product
        public async Task<bool> InsertProduct(InsertProductRequest request)
        {
            int product = context.Products.Max(c => c.Id); 
            // product
            Product pro = new Product
            {
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                Status = false,
                ViewCount = 0,
                CreateDate = DateTime.Now,
            };
            await context.Products.AddAsync(pro);
            await context.SaveChangesAsync();
            // product image
            ProductImage image = new ProductImage
            {
                Caption = request.Caption,
                ImagePath = request.ImagePath,
                Thumbnail = request.Thumbnail,
                ProductId = pro.Id
            };
            await context.ProductImages.AddAsync(image);
            await context.SaveChangesAsync();
            product++;
            foreach (var item in request.ListCategoryId)
            {
                var pic = new ProductInCategory
                {
                    ProductId = product,
                    CategoryId = item
                };
                await context.ProductInCategories.AddAsync(pic);
                await context.SaveChangesAsync();
            }
            return true;
        }

        public async Task<IEnumerable<Product>> SearchProduct(string title)
        {
            return context.Products.Where(c => c.Title.Contains(title));
        }

        public async Task<bool> UpdateStatus(Product productId)
        {
            var model = context.Products.SingleOrDefault(c => c.Id.Equals(productId.Id));
            if (model != null)
            {
                model.Status = true;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateProduct(EditProductRequest request)
        {
            var model = context.Products.SingleOrDefault(c => c.Id.Equals(request.Id));
            if (model != null)
            {
                model.Title = request.Title;
                model.Description = request.Description;
                model.Price = request.Price;
                model.Status = false;
                model.ViewCount = 0;
                model.CreateDate = DateTime.Now;
            }
            // save image
            var image = context.ProductImages.SingleOrDefault(c => c.ProductId.Equals(request.Id));
            if(image != null)
            {
                image.Caption = request.Caption;
                image.ImagePath = request.ImagePath;
                image.Thumbnail = request.Thumbnail;
            }
            await context.SaveChangesAsync();
            return true;
        }
    }
}
