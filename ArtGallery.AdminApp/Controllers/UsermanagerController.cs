using ArtGallery.Data.Entities;
using ArtGallery.ViewModel.System.Admin;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ArtGallery.AdminApp.Controllers
{
    public class UsermanagerController : Controller
    {
        private readonly string url = "http://localhost:5000/api/UsersManager/";
        private readonly string urlProfile = "http://localhost:5000/api/ProfileManager/";
        private readonly HttpClient httpClient = new HttpClient();

        [HttpGet]
        public IActionResult Index()
        {
            var model = JsonConvert.DeserializeObject<IEnumerable<Account>>(httpClient.GetStringAsync(url).Result);
            foreach (var item in model)
            {
                var cart = JsonConvert.DeserializeObject<IEnumerable<Cart>>(httpClient.GetStringAsync(urlProfile + "GetCarts/" + item.Name).Result);
                var order = JsonConvert.DeserializeObject<IEnumerable<Order>>(httpClient.GetStringAsync(urlProfile + "GetOrders/" + item.Name).Result);
                var trans = JsonConvert.DeserializeObject<IEnumerable<Transaction>>(httpClient.GetStringAsync(urlProfile + "GetTransactions/" + item.Name).Result);
                var feedBacks = JsonConvert.DeserializeObject<IEnumerable<FeedBack>>(httpClient.GetStringAsync(urlProfile + "GetFeedBacks/" + item.Name).Result);
                item.Carts = cart.ToList();
                item.Orders = order.ToList();
                item.Transactions = trans.ToList();
                item.FeedBacks = feedBacks.ToList();
            }
            UserModelView userModelView = new UserModelView { Users = model};
            return View(userModelView);
        }

        [HttpPost]
        public IActionResult Index(UserModelView user)
        {
            try
            {
                var model = JsonConvert.DeserializeObject<IEnumerable<Account>>(httpClient.GetStringAsync(url + "searchbyName/" + user.Name).Result);
                UserModelView userModelView = new UserModelView { Users = model };
                return View(userModelView);
            }
            catch (Exception)
            {

                return View("Error");
            }
        }

        public IActionResult Delete(string name)
        {
            try
            {
                var model = httpClient.DeleteAsync(url + name).Result;
                if (model.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();
        }

        public IActionResult Details(string name)
        {
            try
            {
                var model = JsonConvert.DeserializeObject<Account>(httpClient.GetStringAsync(url + name).Result);

                var profile = JsonConvert.DeserializeObject<ProfileUser>(httpClient.GetStringAsync(urlProfile + "getProfileUser/" + name).Result);
                var carts = JsonConvert.DeserializeObject<IEnumerable<Cart>>(httpClient.GetStringAsync(urlProfile + "GetCarts/" + model.Name).Result);
                var orders = JsonConvert.DeserializeObject<IEnumerable<Order>>(httpClient.GetStringAsync(urlProfile + "GetOrders/" + model.Name).Result);
                var transactions = JsonConvert.DeserializeObject<IEnumerable<Transaction>>(httpClient.GetStringAsync(urlProfile + "GetTransactions/" + model.Name).Result);
                var feedBacks = JsonConvert.DeserializeObject<IEnumerable<FeedBack>>(httpClient.GetStringAsync(urlProfile + "GetFeedBacks/" + model.Name).Result);
                UserModelView userModelView = new UserModelView
                {
                    Name = model.Name,
                    User = model,
                    ProfileUser = profile,
                    Carts = carts,
                    Orders = orders,
                    Transactions = transactions,
                    FeedBacks = feedBacks
                };
                return View(userModelView);
            }
            catch (Exception)
            {

                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult Edit(string name)
        {
            var profile = JsonConvert.DeserializeObject<ProfileUser>(httpClient.GetStringAsync(urlProfile + "getProfileUser/" + name).Result);
            ProfileUserModelView PuserModelView = new ProfileUserModelView
            {
                Id = profile.Id,
                FullName = profile.FullName,
                Gender = profile.Gender,
                Address = profile.Address,
                District = profile.District,
                Wards = profile.Wards,
                City = profile.City,
                Hobby = profile.Hobby,
                Avatar = profile.Avatar,
                Email = profile.Email,
                PhoneNumber = profile.PhoneNumber,
                DOB = profile.DOB,
                AccountId = profile.AccountId
            };

            return View(profile);

        }

        [HttpPost]
        public IActionResult Edit(ProfileUser puser)
        {
            try
            {
                var profile = httpClient.PutAsJsonAsync(urlProfile+ "UpdateProfile/",puser).Result;

                if (profile.IsSuccessStatusCode)
                {
                    return View();
                }
                return View("Error");
            }
            catch (Exception)
            {

                return View("Error");
            }
        }
    }
}

