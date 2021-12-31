using ArtGallery.Application.Common;
using ArtGallery.Data.Constants;
using ArtGallery.Data.Entities;
using ArtGallery.ViewModel.System.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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
    public class UsersController : Controller
    {
        private readonly string url = "http://localhost:5000/api/Users/";
        private HttpClient httpClient = new HttpClient();
        public  ITokenService _function;
        public UsersController(ITokenService function)
        {
            _function = function;
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if (!ModelState.IsValid) return View();
            var json = JsonConvert.SerializeObject(loginRequest);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            ResponseApi result = JsonConvert.DeserializeObject<ResponseApi>(await httpClient.PostAsync(url+ "authenticate", httpContent).Result.Content.ReadAsStringAsync());
            if (result.Success)
            {
                var token = result.Data.ToString();
                var userPrincipal = _function.ValidateToken(token);
                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddSeconds(120),
                    IsPersistent = false
                };
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    userPrincipal,
                    authProperties
                    );
                return RedirectToAction("Home", "Home");
            }
            ModelState.AddModelError("loginMessage", result.Message);
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Users");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
        {

            if (!ModelState.IsValid) return View();
            var result = JsonConvert.DeserializeObject<ResponseApi>(await httpClient.PostAsJsonAsync(url+"register", request).Result.Content.ReadAsStringAsync());
            if (result.Success)
            {
                var token = result.Data.ToString();
                var userPrincipal = _function.ValidateToken(token);
                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddSeconds(120),
                    IsPersistent = false
                };
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    userPrincipal,
                    authProperties
                    );
                return RedirectToAction("Home", "Home");
            };
            return View(result.Message);
        }
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            string UserId = "";
            foreach (var item in User.Claims.ToList().Where(c => c.Type.Equals("UserId")))
            {
                UserId = item.Value.ToString();
            }
            ProfileUser result = JsonConvert.DeserializeObject<ProfileUser>(await httpClient.GetAsync(url + "profile/" + UserId).Result.Content.ReadAsStringAsync());
            result.ToString();
            return View(result);
        }
    }
}
