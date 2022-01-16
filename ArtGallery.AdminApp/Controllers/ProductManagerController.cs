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
using ArtGallery.AdminApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace ArtGallery.AdminApp.Controllers
{
    public class ProductManagerController : Controller
    {
        private readonly string url = "http://localhost:5000/api/ProductsManager/";
        private readonly HttpClient httpClient = new HttpClient();

        [HttpGet]
        public IActionResult Index(int pg = 1)
        {
            var model = JsonConvert.DeserializeObject<IEnumerable<Product>>(httpClient.GetStringAsync(url).Result);
            // Check 
            const int pageSize = 10;
            if (pg < 1)
                pg = 1;
            int recsCount = model.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = model.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            ProductModelView productModelView = new ProductModelView { Products = data };
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

        [HttpPost]
        public IActionResult Update(Product Code)
        {
            var model = httpClient.PutAsJsonAsync(url, Code).Result;
            return RedirectToAction("Index");
        }
    }
}
