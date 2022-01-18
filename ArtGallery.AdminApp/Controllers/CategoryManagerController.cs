using ArtGallery.Data.Entities;
using ArtGallery.ViewModel.System.Admin;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using ArtGallery.AdminApp.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ArtGallery.AdminApp.Controllers
{
    public class CategoryManagerController : Controller
    {
        // Check
        private readonly string url = "http://localhost:5000/api/CategoriesManager/";
        private readonly HttpClient httpClient = new HttpClient();

        [HttpGet]
        public IActionResult Index(int pg = 1)
        {
            var model = JsonConvert.DeserializeObject<IEnumerable<Category>>(httpClient.GetStringAsync(url).Result);
            // Check 
            const int pageSize = 10;
            if (pg < 1)
                pg = 1;
            int recsCount = model.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = model.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            CategoryModelView categoryModelView = new CategoryModelView { Categories = data };
            return View(categoryModelView);
        }

        [HttpPost]
        public IActionResult Index(string name, int pg = 1)
        {
            var model = JsonConvert.DeserializeObject<IEnumerable<Category>>(httpClient.GetStringAsync(url + name).Result);
            // Check 
            const int pageSize = 10;
            if (pg < 1)
                pg = 1;
            int recsCount = model.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = model.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            CategoryModelView categoryModelView = new CategoryModelView { Categories = data };
            return View(categoryModelView);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(CategoryModelView category)
        {
            Category cate = new Category { Name = category.Name , Description = category.Description};
            var model = httpClient.PostAsJsonAsync(url, cate).Result;
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                var model = httpClient.DeleteAsync(url + id).Result;
                if (model.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id)
        {
            var cate = JsonConvert.DeserializeObject<Category>(httpClient.GetStringAsync(url + id).Result);
            CategoryModelView category = new CategoryModelView
            {
                Name = cate.Name,
                Description = cate.Description,
                Id = cate.Id
            };
            return View(category);
        }

        [HttpPost]
        public IActionResult Update(CategoryModelView cate)
        {
            Category category = new Category
            {
                Name = cate.Name,
                Description = cate.Description,
                Id = cate.Id
            };
            var model = httpClient.PutAsJsonAsync(url, category).Result;
            return RedirectToAction("Index");
        }
    }
}
