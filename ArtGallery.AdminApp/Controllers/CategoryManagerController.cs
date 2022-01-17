using ArtGallery.Data.Entities;
using ArtGallery.ViewModel.System.Admin;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ArtGallery.AdminApp.Controllers
{
    public class CategoryManagerController : Controller
    {
        //check neu khong chay
        private readonly string url = "http://localhost:5000/api/CategoriesManager/";
        private readonly HttpClient httpClient = new HttpClient();

        [HttpGet]
        public IActionResult Index()
        {
            var model = JsonConvert.DeserializeObject<IEnumerable<Category>>(httpClient.GetStringAsync(url).Result);
            CategoryModelView categoryModelView = new CategoryModelView{ Categories = model };
            return View(categoryModelView);
        }

        [HttpPost]
        public IActionResult Index(string name)
        {
                var model = JsonConvert.DeserializeObject<IEnumerable<Category>>(httpClient.GetStringAsync(url + name).Result);
                CategoryModelView categoryModelView = new CategoryModelView { Categories = model };
                return View(categoryModelView);
        }

        [HttpPost]
        public IActionResult Create(CategoryModelView category)
        {
            Category cate = new Category { Name = category.Name , Description = category.Description};
            var model = httpClient.PostAsJsonAsync(url, cate).Result;
            return RedirectToAction("Index");
        }

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
