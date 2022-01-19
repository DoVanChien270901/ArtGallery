using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Application.System.Admin
{
    public interface IAdminDashboard
    {
        Task<int> GetProductCount();
        Task<int> GetCustomerCount();
        Task<int> GetTransactionsCount();
        Task<int> GetFeedBacksCount();
    }
}
