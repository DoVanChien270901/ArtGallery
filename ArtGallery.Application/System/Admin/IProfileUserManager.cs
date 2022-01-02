using ArtGallery.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Application.System.Admin
{
    public interface IProfileUserManager
    {
        Task<ProfileUser> GetProfileUser(string name);
        Task<Account> GetAccount(string name);
        Task<bool> UpdateProfile(ProfileUser profileUser);
        Task<IEnumerable<Cart>> GetCarts(string name);
        Task<IEnumerable<Order>> GetOrders(string name);
        Task<IEnumerable<Transaction>> GetTransactions(string name);
        Task<IEnumerable<FeedBack>> GetFeedBacks(string name);

    }
}
