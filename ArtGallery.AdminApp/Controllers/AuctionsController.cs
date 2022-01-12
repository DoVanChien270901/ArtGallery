using ArtGallery.Data.Entities;
using ArtGallery.ViewModel.Catalog.Auctions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ArtGallery.AdminApp.Controllers
{
    public class AuctionsController : Controller
    {
        private readonly string url = "http://localhost:5000/api/Auctions/";
        private HttpClient httpClient = new HttpClient();

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Auction> listAuctions = JsonConvert.DeserializeObject<IEnumerable<Auction>>(httpClient.GetStringAsync(url + "GetAllAuctions").Result);
            //HttpContext.Session.SetString("listAuctions", JsonConvert.SerializeObject(listAuctions));
            ViewBag.aucNotover = listAuctions.Where(c => c.EndDateTime > DateTime.Now).ToList();
            //ViewBag.aucGoing = listAuctions.Where(c => c.StartDateTime < DateTime.Now && c.EndDateTime > DateTime.Now).ToList();
            ViewBag.aucEnded = listAuctions.Where(c => c.EndDateTime < DateTime.Now).ToList();
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var model = httpClient.DeleteAsync(url + "DeleteAuction/" + id).Result;
                if (model.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAll", "Auctions");
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            UpdateAuctionRequest auction = JsonConvert.DeserializeObject<UpdateAuctionRequest>(httpClient.GetStringAsync(url + "GetAuction/" + id).Result);
            //ViewBag.auc = auction;

            return View(auction);
        }

        [HttpPost]
        public IActionResult Update(UpdateAuctionRequest request)
        {
            //if (ViewBag.auc== null)
            //{
            //    Auction auction = JsonConvert.DeserializeObject<Auction>(httpClient.GetStringAsync(url + "GetAuction/" + request.Id).Result);
            //    ViewBag.auc = auction;
            //}
            if (!ModelState.IsValid) return View();
            var model = httpClient.PutAsJsonAsync(url + "UpdateAuction/", request).Result;
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public IActionResult WinnerInformation(int id)
        {
            ProfileUser profileUsers = JsonConvert.DeserializeObject<ProfileUser>(httpClient.GetStringAsync(url + "GetWinner/" + id).Result);
            return View(profileUsers);
        }
    }
}
