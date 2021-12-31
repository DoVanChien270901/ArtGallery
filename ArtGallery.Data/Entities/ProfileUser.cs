using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Data.Entities
{
    public class ProfileUser
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string District { get; set; }
        public string Wards { get; set; }
        public string City { get; set; }
        public string Hobby { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public DateTime DOB { get; set; }
        public string AccountId { get; set; }
        public Account Account { get; set; }
        public List<AmountAuction> AmountInAcctions { get; set; }
    }
}
