using ArtGallery.Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.ViewModel.System.Users
{
    public class RegisterRequest
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string District { get; set; }
        public string Wards { get; set; }
        public string City { get; set; }
        public string Hobby { get; set; }
        public IFormFile Avatar { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public DateTime DOB { get; set; }
        public string ConfirmPassword { get; set; }
    }    
}
