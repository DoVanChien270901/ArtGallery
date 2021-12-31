using ArtGallery.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Application.System.Admin
{
    public interface ICategoryServices
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<IEnumerable<Category>> SearchCategory(string catename);
        Task<Category> GetCategory(int cateid);

        Task<bool> InsertCategory(Category category);
        Task<bool> UpdateCategory(Category cateid);
        Task<bool> DeleteCategory(int cateid);
    }
}
