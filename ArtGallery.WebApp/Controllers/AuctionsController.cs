using ArtGallery.Data.Entities;
using ArtGallery.ViewModel.Catalog.Auctions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ArtGallery.WebApp.Models;

namespace ArtGallery.WebApp.Controllers
{
    public class AuctionsController : Controller
    {
        private readonly string url = "http://localhost:5000/api/Auctions/";
        private HttpClient httpClient = new HttpClient();

        // Going 
        [HttpGet]
        public IActionResult GetAuctionGoing(int pg = 1)
        {
            IEnumerable<Auction> listAuctions = JsonConvert.DeserializeObject<IEnumerable<Auction>>(httpClient.GetStringAsync(url + "GetAllAuctions").Result);
            //HttpContext.Session.SetString("listAuctions", JsonConvert.SerializeObject(listAuctions));
            //var aucComming = listAuctions.Where(c => c.StartDateTime > DateTime.Now).ToList();
            var aucGoing = listAuctions.Where(c => c.StartDateTime < DateTime.Now && c.EndDateTime > DateTime.Now).ToList();
            //var aucEnded = listAuctions.Where(c => c.EndDateTime < DateTime.Now).ToList();
            // Check
            const int pageSize2 = 6;
            if (pg < 1)
                pg = 1;
            int recsCount2 = aucGoing.Count();
            var pager2 = new Pager(recsCount2, pg, pageSize2);
            int recSkip2 = (pg - 1) * pageSize2;
            var data2 = aucGoing.Skip(recSkip2).Take(pager2.PageSize).ToList();
            //
            this.ViewBag.Pager = pager2;
            this.ViewBag.aucGoing = data2;
            return View();
        }

        // Comming
        [HttpGet]
        public IActionResult GetAuctionComming(int pg = 1)
        {
            IEnumerable<Auction> listAuctions = JsonConvert.DeserializeObject<IEnumerable<Auction>>(httpClient.GetStringAsync(url + "GetAllAuctions").Result);
            //HttpContext.Session.SetString("listAuctions", JsonConvert.SerializeObject(listAuctions));
            var aucComming = listAuctions.Where(c => c.StartDateTime > DateTime.Now).ToList();
            // Check
            const int pageSize = 6;
            if (pg < 1)
                pg = 1;
            int recsCount = aucComming.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = aucComming.Skip(recSkip).Take(pager.PageSize).ToList();
            //
            this.ViewBag.Pager = pager;
            this.ViewBag.aucComming = data;
            return View();
        }

        // Ended
        [HttpGet]
        public IActionResult GetAuctionEnded(int pg = 1)
        {
            IEnumerable<Auction> listAuctions = JsonConvert.DeserializeObject<IEnumerable<Auction>>(httpClient.GetStringAsync(url + "GetAllAuctions").Result);
            //HttpContext.Session.SetString("listAuctions", JsonConvert.SerializeObject(listAuctions));
            var aucEnded = listAuctions.Where(c => c.EndDateTime < DateTime.Now).ToList();
            // Check
            const int pageSize3 = 6;
            if (pg < 1)
                pg = 1;
            int recsCount3 = aucEnded.Count();
            var pager3 = new Pager(recsCount3, pg, pageSize3);
            int recSkip3 = (pg - 1) * pageSize3;
            var data3 = aucEnded.Skip(recSkip3).Take(pager3.PageSize).ToList();
            //
            this.ViewBag.Pager = pager3;
            this.ViewBag.aucEnded = data3;
            return View();
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult AuctionRoom(int id)
        {
            Auction auction = JsonConvert.DeserializeObject<Auction>(httpClient.GetStringAsync(url + "GetAuction/" + id).Result);
            ViewBag.auc = auction;

            //IEnumerable<Auction> list = JsonConvert.DeserializeObject<IEnumerable<Auction>>(HttpContext.Session.GetString("listAuctions"));
            //HttpContext.Session.Remove("listAuctions");
            //list.SingleOrDefault(c => c.Id.Equals(id));
            //HttpContext.Session.SetString("listAuctions", JsonConvert.SerializeObject(list.SingleOrDefault(c => c.Id.Equals(id))));
            //IEnumerable<Auction> listAuctions = JsonConvert.DeserializeObject<IEnumerable<Auction>>(httpClient.GetStringAsync(url + "GetAllAcutions").Result);
            return View(/*listAuctions.SingleOrDefault(c => c.Id.Equals(id))*/);
        }

        [HttpPost]
        public async Task<IActionResult> AuctionRoom(InsertAmountInAuctionRequest request)
        {
            foreach (var item in User.Claims.ToList().Where(c => c.Type.Equals("UserId")))
            {
                request.AccountId = item.Value.ToString();
            }
            if (ViewBag.auc==null)
            {
                ViewBag.auc = JsonConvert.DeserializeObject<Auction>(httpClient.GetStringAsync(url + "GetAuction/" + request.AuctionId).Result);
            }
            if (!ModelState.IsValid) return View();
            AmountInAuction Aauc = new AmountInAuction
            {
                AuctionId = request.AuctionId,
                NewPrice = request.NewPrice,
                AccountId = request.AccountId
            };
            var json = JsonConvert.SerializeObject(Aauc);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var result = JsonConvert.DeserializeObject<bool>(await httpClient.PostAsync(url + "InsertAmount", httpContent).Result.Content.ReadAsStringAsync());
            return RedirectToAction("AuctionRoom");
        }
    }
}
    