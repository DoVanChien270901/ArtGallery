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
        Task<int> GetCartsCount(string name);
        Task<int> GetOrdersCount(string name);
        Task<int> GetTransactionsCount(string name);
        Task<int> GetFeedBacksCount(string name);
    }
}
