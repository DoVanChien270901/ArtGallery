using ArtGallery.Application.System.Admin;
using ArtGallery.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtGallery.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesManagerController : ControllerBase
    {
        private readonly ICategoryServices categoryServices;
        public CategoriesManagerController(ICategoryServices categoryServices)
        {
            this.categoryServices = categoryServices;
        }

        [HttpDelete("{cateid:int}")]
        public async Task<bool> DeleteCategory(int cateid)
        {
            return await categoryServices.DeleteCategory(cateid);
        }

        [HttpPost]
        public async Task<bool> InsertCategory(Category cate)
        {
            return await categoryServices.InsertCategory(cate);
        }

        [HttpGet]
        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await categoryServices.GetCategories();
        }

        [HttpGet("{cateN}")]
        public async Task<IEnumerable<Category>> SearchCategory(string cateN)
        {
            return await categoryServices.SearchCategory(cateN);
        }

        [HttpGet("{cateid:int}")]
        public async Task<Category> GetCategory(int cateid)
        {
            return await categoryServices.GetCategory(cateid);
        }

        [HttpPut]
        public async Task<bool> UpdateCategory(Category cate)
        {
            return await categoryServices.UpdateCategory(cate);
        }
    }
}
