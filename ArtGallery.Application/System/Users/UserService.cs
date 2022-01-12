using ArtGallery.Application.System.Users;
using ArtGallery.Data.EF;
using ArtGallery.Data.Entities;
using ArtGallery.Data.Enum;
using ArtGallery.ViewModel.System.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Application.System.Users
{
    public class UserService : IUserService
    {
        private ArtGalleryDbContext _db;
        private readonly IConfiguration _config;
        public UserService(ArtGalleryDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        // Authencate 
        public async Task<string> Authencate(LoginRequest loginRequest)
        {
            Account user =_db.Accounts.SingleOrDefault(c=>c.Name==loginRequest.Name && c.Password == loginRequest.Password);
            if (user == null)
            {
                return null;
            }
            ProfileUser profile = _db.ProfileUsers.SingleOrDefault(c=>c.AccountId == user.Name);
            if (user.Roles.ToString()==null)
            {
                user.Roles = Data.Enum.Roleposition.User;
            }
            //Discription token
            var clearms = new[]
            {
                    new Claim(ClaimTypes.Name, profile.FullName),
                    new Claim("UserId", profile.AccountId),
                    new Claim(ClaimTypes.Email, profile.Email),
                    new Claim(ClaimTypes.MobilePhone, profile.PhoneNumber.ToString()),
                    new Claim(ClaimTypes.Role, user.Roles.ToString()),
                    new Claim("TokenId", Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));// Minimun size of key(KeySize) = 126bits(16byte)
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                clearms,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);
            //pare jwtSecurityToken to string
            return new JwtSecurityTokenHandler().WriteToken(token);     
        }

        // Register
        public async Task<string> Register(RegisterRequest registerRequest)
        {
            Account acc = await _db.Accounts.FindAsync(registerRequest.Name);
            if (acc != null) return null;
            acc = new Account
            {
                Name = registerRequest.Name,
                Password = registerRequest.Password,
                Roles = Roleposition.User
            };
            ProfileUser pro = new ProfileUser
            {
                AccountId = registerRequest.Name,
                FullName=registerRequest.FullName,
                Gender=registerRequest.Gender,
                Address = registerRequest.Address, 
                Email = registerRequest.Email,
                PhoneNumber = registerRequest.PhoneNumber,
                DOB = registerRequest.DOB
    };
            await _db.Accounts.AddAsync(acc);
            await _db.ProfileUsers.AddAsync(pro);
            await _db.SaveChangesAsync();
            //Discription token
            var clearms = new[]
            {
                    new Claim(ClaimTypes.Name, registerRequest.FullName),
                    new Claim("UserId", registerRequest.Name),
                    new Claim(ClaimTypes.Email, registerRequest.Email),
                    new Claim(ClaimTypes.MobilePhone, registerRequest.PhoneNumber.ToString()),
                    new Claim(ClaimTypes.Role, Data.Enum.Roleposition.User.ToString()),
                    new Claim("TokenId", Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));// Minimun size of key(KeySize) = 126bits(16byte)
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //create token
            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                clearms,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);
            //pare jwtSecurityToken to string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Get Profile
        public async Task<ProfileUser> GetProfile(string UserId)
        {
            ProfileUser profile = _db.ProfileUsers.SingleOrDefault(c => c.AccountId == UserId);
            return profile;
        }

        // Update Profile
        public async Task<ProfileUser> UpdateProfile(ProfileUser profileUser)
        {
            var profile = _db.ProfileUsers.SingleOrDefault(c=>c.AccountId.Equals(profileUser.AccountId));
            profile.FullName = profileUser.FullName;
            profile.Gender = profileUser.Gender;
            profile.Address = profileUser.Address;
            profile.Email = profileUser.Email;
            profile.DOB = profileUser.DOB;
            profile.PhoneNumber = profileUser.PhoneNumber;
            await _db.SaveChangesAsync();
            return profileUser;
        }
    }
}
