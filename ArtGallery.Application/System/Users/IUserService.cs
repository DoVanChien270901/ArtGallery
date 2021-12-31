using ArtGallery.Data.Entities;
using ArtGallery.ViewModel.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Application.System.Users
{
    public interface IUserService
    {
        Task<string> Authencate(LoginRequest loginRequest);
        Task<string> Register(RegisterRequest registerRequest);
        Task<ProfileUser> GetProfile(string UserId);
    }
}
