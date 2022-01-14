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

namespace ArtGallery.WebApp.Controllers
{
    public class AuctionsController : Controller
    {
        private readonly string url = "http://localhost:5000/api/Auctions/";
        private HttpClient httpClient = new HttpClient();

        [HttpGet]
        public IActionResult GetAllAuctions()
        {
            IEnumerable<Auction> listAuctions = JsonConvert.DeserializeObject<IEnumerable<Auction>>(httpClient.GetStringAsync(url + "GetAllAuctions").Result);
            //HttpContext.Session.SetString("listAuctions", JsonConvert.SerializeObject(listAuctions));
            ViewBag.aucComming = listAuctions.Where(c => c.StartDateTime > DateTime.Now).ToList();
            ViewBag.aucGoing = listAuctions.Where(c => c.StartDateTime < DateTime.Now && c.EndDateTime > DateTime.Now).ToList();
            ViewBag.aucEnded = listAuctions.Where(c => c.EndDateTime < DateTime.Now).ToList();
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
    