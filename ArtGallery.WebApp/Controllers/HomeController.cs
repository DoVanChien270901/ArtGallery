using ArtGallery.Data.Entities;
using ArtGallery.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ArtGallery.WebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly string url = "http://localhost:5000/api/Products/";
        private HttpClient httpClient = new HttpClient();
        //[Authorize(Roles = "User")]
        public IActionResult Home(string cateName)
        {
            string urlcate = "http://localhost:5000/api/CategoriesManager/";
            IEnumerable<Category> Cate = JsonConvert.DeserializeObject<IEnumerable<Category>>(httpClient.GetStringAsync(urlcate).Result);
            ViewBag.cate = Cate;
            if (cateName != null)
            {
                IEnumerable<Product> proincate = JsonConvert.DeserializeObject<IEnumerable<Product>>(httpClient.GetStringAsync(url + "ProductInCategory/" + cateName).Result);
                return View(proincate.Where(c=>c.Status == true));
            }
            IEnumerable<Product> listProducts = JsonConvert.DeserializeObject<IEnumerable<Product>>(httpClient.GetStringAsync(url + "AllProduct").Result);
            return View(listProducts.Where(c => c.Status == true));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
