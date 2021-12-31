using ArtGallery.Application.System.Users;
using ArtGallery.Data.Constants;
using ArtGallery.Data.Entities;
using ArtGallery.ViewModel.System.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtGallery.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("authenticate")]
        public async Task<ResponseApi> Authenticate(LoginRequest request)
        {
            var resultToken = await _userService.Authencate(request);
            if (string.IsNullOrEmpty(resultToken))
                return new ResponseApi
                {
                    Success = false,
                    Message = "User name or password incorrect!!!"
                };
            return new ResponseApi
            {
                Message = "Login success",
                Success = true,
                Data = resultToken
            };
        }
        [HttpGet("register")]
        public async Task<ResponseApi> Register(RegisterRequest request)
        {
            var resultToken = await _userService.Register(request);
            if (string.IsNullOrEmpty(resultToken))
            {
                return new ResponseApi
                {
                    Success = false,
                    Message = "Register Fail!!!"
                };
            }
            return new ResponseApi
            {
                Message = "Login success",
                Success = true,
                Data = resultToken
            };
        }
        [HttpGet("profile/{userId}")]
        public async Task<ProfileUser> Profile(string userId)
        {
            return await _userService.GetProfile(userId);
        }
    }
}
