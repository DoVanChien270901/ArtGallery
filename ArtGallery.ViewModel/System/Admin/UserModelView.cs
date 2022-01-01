using ArtGallery.Data.Entities;
using ArtGallery.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.ViewModel.System.Admin
{
    public class UserModelView
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public Roleposition Roles { get; set; }
        public IEnumerable<Account> Users { get; set; }
        public Account User { get; set; }
        public List<Cart> Carts { get; set; }
        public List<Order> Orders { get; set; }
        public List<Transaction> Transactions { get; set; }
        public List<FeedBack> FeedBacks { get; set; }
        public ProfileUser ProfileUser { get; set; }
    }
}
