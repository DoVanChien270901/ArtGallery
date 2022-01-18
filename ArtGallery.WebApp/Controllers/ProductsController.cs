using ArtGallery.Application.Common;
using ArtGallery.Data.Entities;
using ArtGallery.ViewModel.Catalog.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ArtGallery.WebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly string url = "http://localhost:5000/api/Products/";
        private HttpClient httpClient = new HttpClient();
        private readonly IHttpContextAccessor contextAccessor;
        public ProductsController(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }
        [HttpGet]
        public IActionResult Detail(int id)
        {
            Product signlepro = JsonConvert.DeserializeObject<Product>(httpClient.GetStringAsync(url + "GetProduct/" + id).Result);
            return View(signlepro);
        }
        [HttpGet]
        public IActionResult GetByCate(string cateName)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(InsertProductRequest request)
        {
            
            return View();
        }

    }
}
