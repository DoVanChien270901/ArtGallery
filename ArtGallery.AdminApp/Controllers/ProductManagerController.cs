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
        private readonly string url = "http://localhost:4086/api/Products/";

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
    }
}
