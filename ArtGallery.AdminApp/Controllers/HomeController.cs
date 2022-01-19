using ArtGallery.AdminApp.Models;
using ArtGallery.ViewModel.System.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using ArtGallery.Data.Entities;
using System.Threading.Tasks;

namespace ArtGallery.AdminApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string url = "http://localhost:5000/api/AdminDashboad/";
        private readonly string urlChart = "http://localhost:5000/api/Products/";
        private readonly HttpClient httpClient = new HttpClient();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var user = User.Claims.ToList();
            var a = user[2].Value;
            var cus = JsonConvert.DeserializeObject<int>(httpClient.GetStringAsync(url + "GetCustomerCount").Result);
            var fed = JsonConvert.DeserializeObject<int>(httpClient.GetStringAsync(url + "GetFeedBacksCount").Result);
            var pro = JsonConvert.DeserializeObject<int>(httpClient.GetStringAsync(url + "GetProductCount").Result);
            var trans = JsonConvert.DeserializeObject<int>(httpClient.GetStringAsync(url + "GetTransactionsCount").Result);    
            DashboardModelView view = new DashboardModelView
            {
                CustomerCount = cus,
                FeedBacksCount = fed,
                ProductCount = pro,
                TransactionsCount = trans,       
            };
            return View(view);
        }

        public IActionResult IndexCharts()
        {
            var model = JsonConvert.DeserializeObject<IEnumerable<Product>>
                (httpClient.GetStringAsync(urlChart + "AllProduct").Result);
            return Json(new { JSONList = model });
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
