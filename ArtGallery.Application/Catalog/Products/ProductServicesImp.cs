using ArtGallery.Data.EF;
using ArtGallery.Data.Entities;
using ArtGallery.ViewModel.Catalog.Products;
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
                var listProduct = 
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
                ////Category category = context.Categories.SingleOrDefault(c => c.Name.Equals(cateName));
                ////return context.Products.Where(c => c.CategoryId.Equals(category.Id)).ToList();
                return listProduct.ToList();
            }
            return null;     
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

        public async Task<bool> InsertProduct(InsertProductRequest request)
        {
            int product = context.Products.Max(c => c.Id);
            // add product
            Product pro = new Product
            {
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                Status = request.Status,
                ViewCount = request.ViewCount,
                CreateDate = request.CreateDate,
            };
            await context.Products.AddAsync(pro);
            await context.SaveChangesAsync();
            // add product image
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
            foreach (var item in request.listCategoryId)
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

        public async Task<bool> UpdateProductForAdmin(Product Id)
        {
            var model = context.Products.SingleOrDefault(c => c.Id.Equals(Id.Id));
            if (model != null)
            {
                model.Status = true;
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
