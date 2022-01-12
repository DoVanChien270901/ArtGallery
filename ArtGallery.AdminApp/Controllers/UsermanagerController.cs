using ArtGallery.Data.Entities;
using ArtGallery.ViewModel.System.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
            List<UserModelView> userModelView = new List<UserModelView>();
            foreach (var item in model)
            {
                var cart = JsonConvert.DeserializeObject<int>(httpClient.GetStringAsync(urlProfile + "GetCarts/" + item.Name).Result);
                var order = JsonConvert.DeserializeObject<int>(httpClient.GetStringAsync(urlProfile + "GetOrders/" + item.Name).Result);
                var trans = JsonConvert.DeserializeObject<int>(httpClient.GetStringAsync(urlProfile + "GetTransactions/" + item.Name).Result);
                var feedBacks = JsonConvert.DeserializeObject<int>(httpClient.GetStringAsync(urlProfile + "GetFeedBacks/" + item.Name).Result);
                UserModelView view = new UserModelView
                {
                    Name = item.Name,
                    Password = item.Password,
                    Roles = item.Roles,
                    CartsCount = cart,
                    OrdersCount = order,
                    TransactionsCount = trans,
                    FeedBacksCount = feedBacks
                };
                userModelView.Add(view);
            } 
            return View(userModelView);
        }

        [HttpPost]
        public IActionResult Index(string name)
        {
            var model = JsonConvert.DeserializeObject<IEnumerable<Account>>(httpClient.GetStringAsync(url + "searchbyName/" + name).Result);
            List<UserModelView> userModelView = new List<UserModelView>();
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            foreach (var item in model)
            {
                var cart = JsonConvert.DeserializeObject<int>(httpClient.GetStringAsync(urlProfile + "GetCarts/" + item.Name).Result);
                var order = JsonConvert.DeserializeObject<int>(httpClient.GetStringAsync(urlProfile + "GetOrders/" + item.Name).Result);
                var trans = JsonConvert.DeserializeObject<int>(httpClient.GetStringAsync(urlProfile + "GetTransactions/" + item.Name).Result);
                var feedBacks = JsonConvert.DeserializeObject<int>(httpClient.GetStringAsync(urlProfile + "GetFeedBacks/" + item.Name).Result);
                UserModelView view = new UserModelView
                {
                    Name = item.Name,
                    Password = item.Password,
                    Roles = item.Roles,
                    CartsCount = cart,
                    OrdersCount = order,
                    TransactionsCount = trans,
                    FeedBacksCount = feedBacks
                };
                userModelView.Add(view);
            }
            return View(userModelView);
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
                var carts = JsonConvert.DeserializeObject<int>(httpClient.GetStringAsync(urlProfile + "GetCarts/" + model.Name).Result);
                var orders = JsonConvert.DeserializeObject<int>(httpClient.GetStringAsync(urlProfile + "GetOrders/" + model.Name).Result);
                var transactions = JsonConvert.DeserializeObject<int>(httpClient.GetStringAsync(urlProfile + "GetTransactions/" + model.Name).Result);
                var feedBacks = JsonConvert.DeserializeObject<int>(httpClient.GetStringAsync(urlProfile + "GetFeedBacks/" + model.Name).Result);
                ProfileUserModelView userModelView = new ProfileUserModelView
                {
                    FullName = profile.FullName,
                    Gender = profile.Gender,
                    Address = profile.Address,
                    Hobby = profile.Hobby,
                    Email = profile.Email,
                    PhoneNumber = profile.PhoneNumber,
                    DOB = profile.DOB,
                    AccountId = profile.AccountId,
                    CartsCount = carts,
                    OrdersCount = orders,
                    TransactionsCount = transactions,
                    FeedBacksCount = feedBacks
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
            EditProfileReq PuserModelView = new EditProfileReq
            {
                FullName = profile.FullName,
                Gender = profile.Gender,
                Address = profile.Address,
                Hobby = profile.Hobby,
                Avatar = profile.Avatar,
                Email = profile.Email,
                PhoneNumber = profile.PhoneNumber,
                DOB = profile.DOB,
                AccountId = profile.AccountId
            };
            return View(PuserModelView);
        }

        [HttpPost]
        public IActionResult Edit(EditProfileReq puser)
        {
            try
            {
                ProfileUser pro = new ProfileUser 
                { 
                    FullName = puser.FullName,
                    Gender = puser.Gender,
                    Address = puser.Address,
                    Hobby = puser.Hobby,
                    Email = puser.Email,
                    PhoneNumber = puser.PhoneNumber,
                    DOB = puser.DOB,
                    AccountId = puser.AccountId
                };
                
                var profile = httpClient.PutAsJsonAsync(urlProfile+ "UpdateProfile/",pro).Result;

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

