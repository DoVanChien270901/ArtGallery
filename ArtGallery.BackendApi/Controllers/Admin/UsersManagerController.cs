using ArtGallery.Application.System.Admin;
using ArtGallery.Data.Entities;
using ArtGallery.Data.Enum;
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
    public class UsersManagerController : ControllerBase
    {
        private readonly IUserManagerServices userManager;
        public UsersManagerController(IUserManagerServices userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost("{name}/{pass}")]
        public async Task<bool> CreateUser(string name, string pass, Roleposition role)
        {
            return await userManager.CreateUser(name, pass, role);
        }

        [HttpDelete("{name}")]
        public async Task<bool> DeleteUser(string name)
        {
            return await userManager.DeleteUser(name);
        }

        [HttpPut]
        public async Task<bool> UpdateUser(Account user)
        {
            return await userManager.UpdateUser(user);
        }

        [HttpGet("searchbyName/{uname}")]
        public async Task<IEnumerable<Account>> SearchUsers(string uname)
        {
            return await userManager.SearchUsers(uname);
        }

        [HttpGet]
        public async Task<IEnumerable<Account>> GetUsers()
        {
            return await userManager.GetUsers();
        }

        [HttpGet("{name}")]
        public async Task<Account> GetUser(string name)
        {
            return await userManager.GetUser(name);
        }
        [HttpGet("RequestAdmin/{name}")]
        public Task<bool> RequestAdmin(string name)
        {
            return userManager.RequestAdmin(name);
        }

    }
}
