using ArtGallery.Data.EF;
using ArtGallery.Data.Entities;
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

namespace ArtGallary.Application.System.Users
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
        public async Task<string> Authencate(LoginRequest loginRequest)
        {
            User user = await _db.User.FindAsync(loginRequest.Name);
            if (user == null)
            {
                return "User name is incorrect";
            }
            if (!(user.Password==loginRequest.Password))
            {
                return "User name is incorrect";
            }
            ProfileUser profile = await _db.ProfileUsers.FindAsync(user.Name);
            //var user = from u in _db.User
            //           join p in _db.ProfileUsers
            //           on u.Name equals p.UserId
            //           where u.Name == loginRequest.Name && u.Password == loginRequest.Password
            //           select new
            //           {
            //               FullName = p.FullName,
            //               Email = p.Email,
            //               PhoneNumber = p.PhoneNumber
            //           };
            var secretKeyBytes = _config["Tokens.Issure"];
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, profile.FullName),
                    new Claim(ClaimTypes.Email, profile.Email),
                    new Claim(ClaimTypes.MobilePhone, profile.PhoneNumber.ToString()),
                    //role
                    new Claim("TokenId", Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddSeconds(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKeyBytes)), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
            return new JwtSecurityTokenHandler().WriteToken(token);     
        }
    }
}
