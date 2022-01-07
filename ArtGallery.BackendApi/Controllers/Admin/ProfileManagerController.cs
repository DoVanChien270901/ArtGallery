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

        [HttpGet("GetCarts/{name}")]
        public async Task<IEnumerable<Cart>> GetCarts(string name)
        {
            return await profileUserManager.GetCarts(name);
        }

        [HttpGet("GetOrders/{name}")]
        public async Task<IEnumerable<Order>> GetOrders(string name)
        {
            return await profileUserManager.GetOrders(name);
        }

        [HttpGet("GetTransactions/{name}")]
        public async Task<IEnumerable<Transaction>> GetTransactions(string name)
        {
            return await profileUserManager.GetTransactions(name);
        }

        [HttpGet("GetFeedBacks/{name}")]
        public async Task<IEnumerable<FeedBack>> GetFeedBacks(string name)
        {
            return await profileUserManager.GetFeedBacks(name);
        }

    }
}
