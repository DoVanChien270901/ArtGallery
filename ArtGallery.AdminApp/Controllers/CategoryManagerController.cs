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
        private readonly string url = "http://localhost:4086/api/CategoriesManager/";
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
        public IActionResult Create(string name)
        {
            Category cate = new Category { Name = name };
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

        [HttpPost]
        public IActionResult Update(Category cate)
        {
            var model = httpClient.PutAsJsonAsync(url, cate).Result;
            return RedirectToAction("Index");
        }
    }
}
