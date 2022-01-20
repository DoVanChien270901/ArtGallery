using ArtGallery.Application.System.Admin;
using ArtGallery.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtGallery.BackendApi.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileManagerController : ControllerBase
    {
        private readonly IProfileUserManager profileUserManager;
        public ProfileManagerController(IProfileUserManager profileUserManager)
        {
            this.profileUserManager = profileUserManager;
        }

        [HttpGet("getProfileUser/{name}")]
        public async Task<ProfileUser> GetProfileUser(string name)
        {
            return await profileUserManager.GetProfileUser(name);
        }

        [HttpGet("GetAccount/{name}")]
        public async Task<Account> GetAccount(string name)
        {
            return await profileUserManager.GetAccount(name);
        }

        [HttpPut("UpdateProfile/")]
        public async Task<bool> UpdateProfile(ProfileUser profileUser)
        {
            return await profileUserManager.UpdateProfile(profileUser);
        }


        [HttpGet("GetOrders/{name}")]
        public async Task<int> GetOrders(string name)
        {
            return await profileUserManager.GetOrdersCount(name);
        }

        [HttpGet("GetTransactions/{name}")]
        public async Task<int> GetTransactions(string name)
        {
            return await profileUserManager.GetTransactionsCount(name);
        }

        [HttpGet("GetFeedBacks/{name}")]
        public async Task<int> GetFeedBacks(string name)
        {
            return await profileUserManager.GetFeedBacksCount(name);
        }

    }
}
