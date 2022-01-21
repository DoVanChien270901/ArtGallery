using ArtGallery.Application.Common;
using ArtGallery.Data.Constants;
using ArtGallery.Data.Entities;
using ArtGallery.ViewModel.System.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.AdminApp.Controllers
{
    public class AccountsController : Controller
    {
        private readonly string url = "http://localhost:5000/api/Users/";
        private HttpClient httpClient = new HttpClient();

        public ITokenService _function;
        public AccountsController(ITokenService function)
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
            ResponseApi result = JsonConvert.DeserializeObject<ResponseApi>(await httpClient.PostAsync(url + "authenticate", httpContent).Result.Content.ReadAsStringAsync());
            if (result.Success)
            {
                var token = result.Data.ToString();
                var userPrincipal = _function.ValidateToken(token);
                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),
                    IsPersistent = false
                };
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    userPrincipal,
                    authProperties
                    );
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("loginMessage", result.Message);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Accounts");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request, string gender)
        {
            request.Gender = gender;
            request.Role = Data.Enum.Roleposition.RAdmin;
            if (!ModelState.IsValid) return View();
            var result = JsonConvert.DeserializeObject<ResponseApi>(await httpClient.PostAsJsonAsync(url + "register", request).Result.Content.ReadAsStringAsync());
            if (result.Success)
            {
                var token = result.Data.ToString();
                var userPrincipal = _function.ValidateToken(token);
                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),
                    IsPersistent = false
                };
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    userPrincipal,
                    authProperties
                    );
                ViewBag.msg= "Successful registration please wait for account confirmation within 24 hours";
                return View();
            };
            ModelState.AddModelError("registerMessage", result.Message);
            return View();
        }
    }
}
