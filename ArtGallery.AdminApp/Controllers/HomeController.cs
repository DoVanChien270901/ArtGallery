using ArtGallery.AdminApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ArtGallery.Data.Entities;
using Newtonsoft.Json;
using System.Net.Http;
using ArtGallery.ViewModel.Catalog.Products;

namespace ArtGallery.AdminApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly string url = "http://localhost:5000/api/Products/";
        private HttpClient httpClient = new HttpClient();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult IndexCharts()
        {
            IEnumerable<Product> listProducts = JsonConvert.DeserializeObject<IEnumerable<Product>>(httpClient.GetStringAsync(url + "AllProduct").Result);
            var model = listProducts.Where(c => c.Status == false).ToList();
            return Json(new { JSONList = model });
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var user = User.Claims.ToList();
            var a = user[2].Value; 
            return View();
        }

        public IActionResult Privacy()
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
