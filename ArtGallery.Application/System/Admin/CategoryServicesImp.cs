using ArtGallery.Data.EF;
using ArtGallery.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Application.System.Admin
{
    public class CategoryServicesImp : ICategoryServices
    {
        private readonly ArtGalleryDbContext context;
        public CategoryServicesImp(ArtGalleryDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> DeleteCategory(int cateid)
        {
            var cate = context.Categories.SingleOrDefault(c=>c.Id.Equals(cateid));
            if (cate != null)
            {
                context.Categories.Remove(cate);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return context.Categories.ToList();
        }

        public async Task<Category> GetCategory(int cateid)
        {
            return context.Categories.SingleOrDefault(c=>c.Id.Equals(cateid));
        }

        public async Task<bool> InsertCategory(Category category)
        {
            var cate = context.Categories.SingleOrDefault(c => c.Name.Equals(category.Name));
            if (cate == null)
            {
                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        //seach with name
        public async Task<IEnumerable<Category>> SearchCategory(string catename)
        {
            return context.Categories.Where(c=>c.Name.Contains(catename));
        }

        public async Task<bool> UpdateCategory(Category cateid)
        {
            var cate = context.Categories.SingleOrDefault(c=>c.Id.Equals(cateid.Id));
            if (cate != null)
            {
                cate.Name = cateid.Name;
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
