using ArtGallery.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Application.System.Admin
{
    public class AdminDashboardServices : IAdminDashboard
    {
        private readonly ArtGalleryDbContext context;
        public AdminDashboardServices(ArtGalleryDbContext context)
        {
            this.context = context;
        }
        public async Task<int> GetCustomerCount()
        {
            var model = context.Accounts.Where(c=>c.Roles.Equals(Data.Enum.Roleposition.User)).ToList();
            try
            {
                return model.Count();
            }
            catch (ArgumentNullException)
            {

                return 0;
            }
        }

        public async Task<int> GetFeedBacksCount()
        {
            var model = context.FeedBacks.ToList();
            try
            {
                return model.Count();
            }
            catch (ArgumentNullException)
            {

                return 0;
            }
        }

        public async Task<int> GetProductCount()
        {
            var model = context.Products.ToList();
            try
            {
                return model.Count();
            }
            catch (ArgumentNullException)
            {

                return 0;
            }
        }

        public async Task<int> GetTransactionsCount()
        {
            var model = context.Transactions.ToList();
            try
            {
                return model.Count();
            }
            catch (ArgumentNullException)
            {

                return 0;
            }
        }
    }
}
