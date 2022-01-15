using ArtGallery.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ArtGallery.WebApp.Controllers
{
    public class ManagerStoresController : Controller
    {
        private readonly string url = "http://localhost:5000/api/";
        private HttpClient httpClient = new HttpClient();
        [Authorize]
        [HttpGet]
        public IActionResult GetProduct()
        {
            string UserId = "";
            foreach (var item in User.Claims.ToList().Where(c => c.Type.Equals("UserId")))
            {
                UserId = item.Value.ToString();
            }
            IEnumerable<Product> listProducts = JsonConvert.DeserializeObject<IEnumerable<Product>>(httpClient.GetStringAsync(url + "Products/AllProduct").Result);
            ViewBag.products = listProducts.Where(c => c.AccountId == UserId).ToList();
            return View();
        }
        //[HttpGet]
        //public IActionResult GetAuction()
        //{
        //    string UserId = "";
        //    foreach (var item in User.Claims.ToList().Where(c => c.Type.Equals("UserId")))
        //    {
        //        UserId = item.Value.ToString();
        //    }
        //    IEnumerable<Product> listProducts = JsonConvert.DeserializeObject<IEnumerable<Product>>(httpClient.GetStringAsync(url + "Products/AllProduct").Result);
        //    ViewBag.products = listProducts.Where(c => c.AccountId == UserId).ToList();
        //    return View();
        //}
    }
}
