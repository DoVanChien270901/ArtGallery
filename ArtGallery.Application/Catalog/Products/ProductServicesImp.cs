using ArtGallery.Application.Common;
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
        private readonly IStorageService _storageService;
        private readonly ArtGalleryDbContext context;
        public ProductServicesImp(ArtGalleryDbContext context, IStorageService storageService)
        {
            _storageService = storageService;
            this.context = context;
        }

        // Product In Category
        public async Task<List<Product>> ProductInCategory(string cateName)
        {
            if (cateName != null)
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
                List<Product> ListProduct =
                    (from p in context.Products
                     join pic in context.ProductInCategories on p.Id equals pic.ProductId
                     join c in context.Categories on pic.CategoryId equals c.Id
                     where c.Name.Equals(cateName)
                     select new Product
                     {
                         Id = p.Id,
                         Title = p.Title,
                         Description = p.Description,
                         Price = p.Price,
                         Status = p.Status,
                         ViewCount = p.ViewCount,
                         CreateDate = p.CreateDate,
                     }).ToList();
                List<ProductImage> images = context.ProductImages.ToList();
                foreach (Product item in ListProduct)
                {
                    List<ProductImage> listimg = images.Where(a => a.ProductId.Equals(item.Id)).ToList();
                    if (listimg != null)
                    {
                        item.ProductImages = listimg;
                        foreach (ProductImage img in item.ProductImages)
                        {
                            img.ImagePath = await _storageService.GetFileUrl(img.ImagePath);
                        }
                        item.ProductImages = listimg;
                    }
                }
                return ListProduct;
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
            Product product = await context.Products.FirstOrDefaultAsync(c=>c.Id.Equals(productId));
            List<ProductImage> images = context.ProductImages.Where(c=>c.ProductId.Equals(productId)).ToList();
            foreach (ProductImage item in images)
            {
                item.ImagePath = await _storageService.GetFileUrl(item.ImagePath);
            }
            product.ProductImages = images;
            return product;
        }

        // Get List
        //public async Task<IEnumerable<Product>> GetProducts()
        //{
        //    return context.Products.ToList();
        //}
        public async Task<IEnumerable<Product>> GetProducts()
        {
            //get product and thumbnail
            IEnumerable<Product> model = (from pro in context.Products
                                          join img in context.ProductImages on pro.Id equals img.ProductId
                                          where img.Thumbnail == true
                                          select new Product
                                          {
                                              Id = pro.Id,
                                              Title = pro.Title,
                                              Description = pro.Description,
                                              Price = pro.Price,
                                              Status = pro.Status,
                                              ViewCount = pro.ViewCount,
                                              CreateDate = pro.CreateDate,
                                              AccountId = pro.AccountId,
                                              ProductImages = new List<ProductImage>
                                                {
                                                    new ProductImage
                                                    {
                                                        Caption = img.Caption,
                                                        ImagePath = img.ImagePath,
                                                        Thumbnail = img.Thumbnail
                                                    }
                                                }
                                          }).ToList();
            foreach (var item in model)
            {
                item.ProductImages[0].ImagePath = await _storageService.GetFileUrl(item.ProductImages[0].ImagePath);
            }
            //get product and all img
            //List<Product> products = context.Products.ToList();
            //List<ProductImage> images = context.ProductImages.ToList();
            //foreach (Product item in products)
            //{
            //    List<ProductImage> listimg = images.Where(a => a.ProductId.Equals(item.Id)).ToList();
            //    if (listimg !=null)
            //    {
            //        item.ProductImages=listimg;
            //    }  
            //}

            return model;
        }
        // Insert Product
        public async Task<bool> InsertProduct(InsertProductRequest request)
        {
            // product
            Product pro = new Product
            {
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                //Status = false,
                //ViewCount = 0,
                CreateDate = DateTime.Now,
                AccountId = request.AccountId
            };
            // product thumbnail
            if (request.Thumbnail != null)
            {
                pro.ProductImages = new List<ProductImage>()
                {
                    new ProductImage
                    {
                        Caption = request.Thumbnail.FileName,
                        ImagePath = await this.SaveFile(request.Thumbnail),
                        Thumbnail = true,
                    }
                };
            }
            //product image
            if (request.Images != null)
            {
                foreach (IFormFile item in request.Images)
                {
                    ProductImage images =
                            new ProductImage
                            {
                                Caption = item.FileName,
                                ImagePath = await this.SaveFile(item),
                                Thumbnail = false,
                            };
                    pro.ProductImages.Add(images);
                }
            }
            await context.Products.AddAsync(pro);
            await context.SaveChangesAsync();
            //productincate
            int product = context.Products.Max(c => c.Id);
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
                model.CreateDate = DateTime.Now;
            }
            // save image
            
            if(request.Thumbnail != null)
            {
                var image = await context.ProductImages.FirstOrDefaultAsync(x => x.Thumbnail == true && x.ProductId == request.Id);
                image.Caption = request.Caption;
                image.ImagePath = await this.SaveFile(request.Thumbnail);
                context.ProductImages.Update(image);
            }
            await context.SaveChangesAsync();
            return true;
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFile(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}
