using ArtGallery.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ArtGallery.AdminApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly string url = "http://localhost:5000/api/Carts/";
        private HttpClient httpClient = new HttpClient();
        [HttpGet]
        public IActionResult SuccessfulDelivery()
        {
            List<Order> listOrder = JsonConvert.DeserializeObject<List<Order>>(httpClient.GetStringAsync(url + "Get").Result);
            listOrder = listOrder.Where(c => c.Status == true).ToList();
            return View(listOrder);
        }
        [HttpGet]
        public IActionResult Ordering()
        {
            List<Order> listOrder = JsonConvert.DeserializeObject<List<Order>>(httpClient.GetStringAsync(url + "Get").Result);
            listOrder = listOrder.Where(c => c.Status == false).ToList();
            return View(listOrder);
        }
        [HttpGet]
        public IActionResult UpdateStatus(int id)
        {
            var result = JsonConvert.DeserializeObject<bool>(httpClient.GetStringAsync(url + "Update/" + id).Result);
            if (result == true)
            {
                return RedirectToAction("Ordering", "Order");
            }
            return BadRequest();
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var result = httpClient.DeleteAsync(url + "Delete/" + id).Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("SuccessfulDelivery" , "Order");
            }
            return BadRequest();
        }
    }
}
