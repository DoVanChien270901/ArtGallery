﻿using ArtGallery.Data.EF;
using ArtGallery.Data.Entities;
using ArtGallery.Data.Enum;
using ArtGallery.ViewModel.System.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Application.System.Admin
{
    public class UserManagerServicesImp : IUserManagerServices
    {
        private readonly ArtGalleryDbContext context;
        public UserManagerServicesImp(ArtGalleryDbContext context)
        {
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
            if (us != null)
            {
                context.Accounts.Remove(us);
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
