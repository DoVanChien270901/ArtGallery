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
<<<<<<< HEAD
        private readonly string url = "http://localhost:5000/api/Products/";
=======
        private readonly string url = "http://localhost:4086/api/Products/";

>>>>>>> 5aab98afb5cf3631d45648238a059d935029aca9
        private readonly HttpClient httpClient = new HttpClient();

        [HttpGet]
        public IActionResult Index(string title)
        {
            if (title != null)
            {
                IEnumerable<Product> productintitle = JsonConvert.DeserializeObject<IEnumerable<Product>>(httpClient.GetStringAsync(url + "AllProduct").Result);
                return View(productintitle.Where(c => c.Status == true && c.Title.Contains(title)));
            }
            IEnumerable<Product> listProducts = JsonConvert.DeserializeObject<IEnumerable<Product>>(httpClient.GetStringAsync(url + "AllProduct").Result);
            return View(listProducts.Where(c => c.Status == true));
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            Product signlepro = JsonConvert.DeserializeObject<Product>(httpClient.GetStringAsync(url + "GetProduct/" + id).Result);
            return View(signlepro);
        }
        [HttpGet]
        public IActionResult GetRequestUser(string title)
        {
            if (title != null)
            {
                IEnumerable<Product> productintitle = JsonConvert.DeserializeObject<IEnumerable<Product>>(httpClient.GetStringAsync(url + "AllProduct").Result);
                return View(productintitle.Where(c => c.Status == false && c.Title.Contains(title)));
            }
            IEnumerable<Product> listProducts = JsonConvert.DeserializeObject<IEnumerable<Product>>(httpClient.GetStringAsync(url + "AllProduct").Result);
            return View(listProducts.Where(c => c.Status == false));
        }
        [HttpGet]
        public IActionResult EditStatus(int id)
        {
           var result = JsonConvert.DeserializeObject<bool>(httpClient.GetStringAsync(url + "UpdateStatus/" + id).Result);
           return RedirectToAction("Index", "ProductManager");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var result = httpClient.DeleteAsync(url + "DeleteProduct/" + id).Result;
            return RedirectToAction("Index", "ProductManager");
        }
    }
}
