using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.ViewModel.System.Admin
{
    public class ProfileUserModelView
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Hobby { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DOB { get; set; }
        public string AccountId { get; set; }
        public int CartsCount { get; set; }
        public int OrdersCount { get; set; }
        public int TransactionsCount { get; set; }
        public int FeedBacksCount { get; set; }
    }
}
