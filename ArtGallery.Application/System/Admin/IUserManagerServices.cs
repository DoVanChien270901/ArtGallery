using ArtGallery.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Application.System.Admin
{
    public interface IUserManagerServices
    {
        Task<Account> GetUser(string uname);
        Task<IEnumerable<Account>> GetUsers();
        Task<bool> CreateUser(string name, string pass);
        Task<bool> UpdateUser(Account uname);
        Task<bool> DeleteUser(string uname);
        Task<IEnumerable<Account>> SearchUsers(string uname);
    }
}
