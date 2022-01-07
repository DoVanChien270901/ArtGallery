using ArtGallery.Data.EF;
using ArtGallery.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Application.System.Admin
{
    public class ProfileUserManagerImp : IProfileUserManager
    {
        private readonly ArtGalleryDbContext context;
        public ProfileUserManagerImp(ArtGalleryDbContext context)
        {
            this.context = context;
        }

        public async Task<Account> GetAccount(string name)
        {
            return context.Accounts.SingleOrDefault(a=>a.Name.Equals(name));
        }

        public async Task<IEnumerable<Cart>> GetCarts(string name)
        {
            return context.Carts.Where(c=>c.AccountId.Equals(name)).ToList();
        }

        public async Task<IEnumerable<FeedBack>> GetFeedBacks(string name)
        {
            return context.FeedBacks.Where(c => c.AccountId.Equals(name)).ToList();
        }

        public async Task<IEnumerable<Order>> GetOrders(string name)
        {
            return context.Orders.Where(c => c.AccountId.Equals(name)).ToList();
        }

        public async Task<ProfileUser> GetProfileUser(string name)
        {
            return context.ProfileUsers.SingleOrDefault(c => c.AccountId.Equals(name));
        }

        public async Task<IEnumerable<Transaction>> GetTransactions(string name)
        {
            return context.Transactions.Where(c => c.AccountId.Equals(name)).ToList();
        }

        public async Task<bool> UpdateProfile(ProfileUser profileUser)
        {
            var model = context.ProfileUsers.SingleOrDefault(p=>p.AccountId.Equals(profileUser.AccountId));
            if (model != null)
            {
                model.FullName = profileUser.FullName;
                model.Gender = profileUser.Gender;
                model.Address = profileUser.Address;
                model.Wards = profileUser.Wards;
                model.City = profileUser.City;
                model.Hobby = profileUser.Hobby;
                model.Avatar = profileUser.Avatar;
                model.Email = profileUser.Email;
                model.PhoneNumber = profileUser.PhoneNumber;
                model.DOB = profileUser.DOB;
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
