using ArtGallery.ViewModel.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallary.Application.System.Users
{
    public interface IUserService 
    {
        Task<string> Authencate(LoginRequest loginRequest);
        //Task<bool> Register(LoginRequest loginRequest);
    }
}
