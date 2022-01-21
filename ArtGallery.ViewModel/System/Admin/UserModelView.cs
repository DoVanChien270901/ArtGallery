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
        public int OrdersCount { get; set; }
        public int TransactionsCount { get; set; }
        public int FeedBacksCount { get; set; }
    }
}
