using ArtGallery.Data.Entities;
using ArtGallery.ViewModel.Catalog.Auctions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using ArtGallery.AdminApp.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ArtGallery.AdminApp.Controllers
{
    public class AuctionsController : Controller
    {
        private readonly string url = "http://localhost:5000/api/Auctions/";
        private HttpClient httpClient = new HttpClient();

        [HttpGet]
        public IActionResult GetAll(int pg = 1)
        {
            IEnumerable<Auction> listAuctions = JsonConvert.DeserializeObject<IEnumerable<Auction>>(httpClient.GetStringAsync(url + "GetAllAuctions").Result);
            //HttpContext.Session.SetString("listAuctions", JsonConvert.SerializeObject(listAuctions));
            var aucNotover = listAuctions.Where(c => c.EndDateTime > DateTime.Now).ToList();
            var aucEnded = listAuctions.Where(c => c.EndDateTime < DateTime.Now).ToList();
            //ViewBag.aucNotover = listAuctions.Where(c => c.EndDateTime > DateTime.Now).ToList();
            // Check
            const int pageSize = 6;
            const int pageSize2 = 6;
            //
            if (pg < 1)
                pg = 1;
            //
            int recsCount = aucNotover.Count();
            int recsCount2 = aucEnded.Count();
            //
            var pager = new Pager(recsCount, pg, pageSize);
            var pager2 = new Pager(recsCount2, pg, pageSize2);
            //
            int recSkip = (pg - 1) * pageSize;
            int recSkip2 = (pg - 1) * pageSize2;
            //
            var data = aucNotover.Skip(recSkip).Take(pager.PageSize).ToList();
            var data2 = aucEnded.Skip(recSkip2).Take(pager2.PageSize).ToList();
            //
            this.ViewBag.Pager = pager;
            this.ViewBag.Pager = pager2;
            //
            this.ViewBag.aucNotover = data;
            this.ViewBag.aucEnded = data2;
            //ViewBag.aucGoing = listAuctions.Where(c => c.StartDateTime < DateTime.Now && c.EndDateTime > DateTime.Now).ToList();  
            return View();
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public IActionResult WinnerInformation(int id)
        {
            ProfileUser profileUsers = JsonConvert.DeserializeObject<ProfileUser>(httpClient.GetStringAsync(url + "GetWinner/" + id).Result);
            return View(profileUsers);
        }
    }
}
