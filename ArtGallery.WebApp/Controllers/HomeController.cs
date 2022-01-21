using ArtGallery.Data.Entities;
using ArtGallery.ViewModel.Catalog.Email;
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
    public class HomeController : Controller
    {
        private readonly string url = "http://localhost:5000/api/Products/";
        private HttpClient httpClient = new HttpClient();
        //[Authorize(Roles = "User")]
        public IActionResult Home(string search, string cateName, int pg = 1)
        {
            string urlcate = "http://localhost:5000/api/CategoriesManager/";
            IEnumerable<Category> Cate = JsonConvert.DeserializeObject<IEnumerable<Category>>(httpClient.GetStringAsync(urlcate).Result);
            ViewBag.cate = Cate;
            if (cateName != null)
            {
                IEnumerable<Product> proincate = JsonConvert.DeserializeObject<IEnumerable<Product>>(httpClient.GetStringAsync(url + "ProductInCategory/" + cateName).Result);
                return View(proincate.Where(c => c.Status == true));
            }
            IEnumerable<Product> listProducts = JsonConvert.DeserializeObject<IEnumerable<Product>>(httpClient.GetStringAsync(url + "AllProduct").Result);
            var model = listProducts.Where(c => c.Status == true);
            if (search!= null)
            {
                model = model.Where(c => c.Title.Contains(search));
            }
            const int pageSize = 12;
            if (pg < 1)
                pg = 1;
            int recsCount = model.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = model.Skip(recSkip).Take(pager.PageSize).ToList();
            //
            this.ViewBag.Pager = pager;
            this.ViewBag.aucComming = data;
            return View(data);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            string urlcate = "http://localhost:5000/api/CategoriesManager/";
            IEnumerable<Category> Cate = JsonConvert.DeserializeObject<IEnumerable<Category>>(httpClient.GetStringAsync(urlcate).Result);
            ViewBag.cate = Cate;
            Product signlepro = JsonConvert.DeserializeObject<Product>(httpClient.GetStringAsync(url + "GetProduct/" + id).Result);
            return View(signlepro);
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            ContactModelView contact = new ContactModelView();
            return View(contact);
        }
        [HttpPost]
        public IActionResult Contact(ContactModelView contact)
        {
            string urlmail = "http://localhost:5000/api/Mail/";
            var send = JsonConvert.DeserializeObject<bool>(httpClient.GetStringAsync(urlmail + "ContactUs/" + contact.FromMail + "/" + contact.Message).Result);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
