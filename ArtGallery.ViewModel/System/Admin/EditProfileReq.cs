using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.ViewModel.System.Admin
{
    public class EditProfileReq
    {
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Hobby { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DOB { get; set; }
        public string AccountId { get; set; }
    }
}
