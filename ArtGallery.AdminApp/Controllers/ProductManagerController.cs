using ArtGallery.Data.Entities;
using ArtGallery.ViewModel.System.Admin;
using ArtGallery.ViewModel.Catalog.Products;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ArtGallery.AdminApp.Controllers
{
    public class ProductManagerController : Controller
    {
        private readonly string url = "http://localhost:5000/api/ProductsManager/";
        private readonly HttpClient httpClient = new HttpClient();

        [HttpGet]
        public IActionResult Index()
        {
            var model = JsonConvert.DeserializeObject<IEnumerable<Product>>(httpClient.GetStringAsync(url).Result);
            ProductModelView productModelView = new ProductModelView { Products = model };
            return View(productModelView);
        }

        [HttpPost]
        public IActionResult Index(string title)
        {
            var model = JsonConvert.DeserializeObject<IEnumerable<Product>>(httpClient.GetStringAsync(url + title).Result);
            ProductModelView productModelView = new ProductModelView { Products = model };
            return View(productModelView);
        }

        //[HttpPost]
        //public IActionResult Create(int Id)
        //{
        //    Product product = new Product { Id = Id };
        //    var model = httpClient.PostAsJsonAsync(url, product).Result;
        //    return RedirectToAction("Index");
        //}

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
        public IActionResult Update(Product Code)
        {
            var model = httpClient.PutAsJsonAsync(url, Code).Result;
            return RedirectToAction("Index");
        }
    }
}
