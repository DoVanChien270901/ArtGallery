using ArtGallery.Data.EF;
using ArtGallery.Data.Entities;
using ArtGallery.ViewModel.System.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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

        public async Task<string> Authencate(LoginRequest loginRequest)
        {
            Account user = context.Accounts.SingleOrDefault(c => c.Name == loginRequest.Name && c.Password == loginRequest.Password);
            if (user == null)
            {
                return null;
            }
            ProfileUser profile = context.ProfileUsers.SingleOrDefault(c => c.AccountId == user.Name);
            if (user.Roles.ToString() == null)
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

        public async Task<bool> CreateUser(string name, string pass)
        {
            var us = context.Accounts.SingleOrDefault(c => c.Name.Equals(name));
            if (us == null)
            {
                Account user = new Account { Name = name, Password = pass};
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
            return context.Accounts.SingleOrDefault(u=>u.Name.Equals(uname));
        }

        public async Task<IEnumerable<Account>> GetUsers()
        {
            return context.Accounts.ToList();
        }

        public async Task<string> Register(RegisterRequest registerRequest)
        {
            Account acc = new Account
            {
                Name = registerRequest.Name,
                Password = registerRequest.Password
            };
            ProfileUser pro = new ProfileUser
            {
                AccountId = registerRequest.Name,
                FullName = registerRequest.FullName,
                Gender = registerRequest.Gender,
                Address = registerRequest.Address,
                Email = registerRequest.Email,
                PhoneNumber = registerRequest.PhoneNumber,
                DOB = registerRequest.DOB
            };
            await context.Accounts.AddAsync(acc);
            await context.ProfileUsers.AddAsync(pro);
            await context.SaveChangesAsync();
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
