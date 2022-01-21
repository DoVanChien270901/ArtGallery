using ArtGallery.Data.EF;
using ArtGallery.Data.Entities;
using ArtGallery.ViewModel.System.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ArtGallery.Data.Enum;
using ArtGallery.ViewModel.System.Admin;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Application.System.Admin
{
    public class UserManagerServicesImp : IUserManagerServices
    {
        private readonly ArtGalleryDbContext context;
        private readonly IConfiguration _config;
        public UserManagerServicesImp(ArtGalleryDbContext context, IConfiguration config)
        {
            _config = config;
            this.context = context;
        }

        public async Task<bool> CreateUser(string name, string pass, Roleposition role)
        {
            var us = context.Accounts.SingleOrDefault(c => c.Name.Equals(name));
            if (us == null)
            {
                Account user = new Account { Name = name, Password = pass, Roles = role};
                await context.Accounts.AddAsync(user);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteUser(string uname)
        {
            var us = context.Accounts.SingleOrDefault(c => c.Name.Equals(uname));
            var pr = context.ProfileUsers.SingleOrDefault(c => c.AccountId.Equals(uname));
            if (us != null)
            {
                context.Accounts.Remove(us);
                context.ProfileUsers.Remove(pr);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Account> GetUser(string uname)
        {
            var model = context.Accounts.SingleOrDefault(a=>a.Name.Equals(uname));
            return model;
        }

        public async Task<IEnumerable<Account>> GetUsers()
        {
            return context.Accounts.ToList();
        }

        public async Task<bool> RequestAdmin(string uname)
        {
            var acc = await context.Accounts.FindAsync(uname);
            if (acc!= null)
            {
                acc.Roles = Roleposition.Admin;
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Account>> SearchUsers(string uname)
        {
            return context.Accounts.Where(u=>u.Name.Contains(uname));
        }

        public async Task<bool> UpdateUser(Account user)
        {
            var us = context.Accounts.SingleOrDefault(c => c.Name.Equals(user.Name));
            if (us != null)
            {
                us.Name = user.Name;
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
