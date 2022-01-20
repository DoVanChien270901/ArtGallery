using ArtGallery.Data.Entities;
using ArtGallery.ViewModel.Catalog.Carts;
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
    public class CartsController : Controller
    {
        private readonly string url = "http://localhost:5000/api/Products/";
        private HttpClient httpClient = new HttpClient();
        public IActionResult AddToCart(int id)
        {
            if (HttpContext.Session.GetString("carts") == null)
            {
                Product signlepro = JsonConvert.DeserializeObject<Product>(httpClient.GetStringAsync(url + "GetProduct/" + id).Result);
                List<SessionCart> cart = new List<SessionCart>
                {
                    new SessionCart
                    {
                          Id = signlepro.Id,
                          Title = signlepro.Title,
                          Price = signlepro.Price,
                          PathImg = signlepro.ProductImages[0].ImagePath
                    }

                };
                HttpContext.Session.SetString("carts", JsonConvert.SerializeObject(cart));
                TempData["msgcart"] = "The product has been added to cart";
                return RedirectToAction("Home", "Home");
            }
            var carts = JsonConvert.DeserializeObject<List<SessionCart>>(HttpContext.Session.GetString("carts"));
            var cartif = carts.Where(c => c.Id == id).ToList();
            if (cartif.Count == 0)
            {
                Product signlepro = JsonConvert.DeserializeObject<Product>(httpClient.GetStringAsync(url + "GetProduct/" + id).Result);
                SessionCart cart = new SessionCart
                {
                    Id = signlepro.Id,
                    Title = signlepro.Title,
                    Price = signlepro.Price,
                    PathImg = signlepro.ProductImages[0].ImagePath
                };
                carts.Add(cart);
                HttpContext.Session.SetString("carts", JsonConvert.SerializeObject(carts));
                TempData["msgcart"] = "The product has been added to cart";
                return RedirectToAction("Home", "Home");
            }
            TempData["msgcarterro"] = "The product already exists in the cart";
            return RedirectToAction("Home", "Home");
        }
        [HttpGet]
        public IActionResult GetCart()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var carts = JsonConvert.DeserializeObject<List<SessionCart>>(HttpContext.Session.GetString("carts")).ToList();

            carts.Remove(carts.SingleOrDefault(c=>c.Id == id));
            HttpContext.Session.SetString("carts", JsonConvert.SerializeObject(carts));
            return RedirectToAction("GetCart", "Carts");
        }
        [HttpGet]
        public IActionResult Order()
        {

            return View();
        }
    }
}
