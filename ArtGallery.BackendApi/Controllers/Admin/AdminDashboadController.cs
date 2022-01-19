using ArtGallery.Application.System.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtGallery.BackendApi.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminDashboadController : ControllerBase
    {
        private readonly IAdminDashboard dashboard;
        public AdminDashboadController(IAdminDashboard dashboard)
        {
            this.dashboard = dashboard;
        }

        [HttpGet("GetCustomerCount")]
        public async Task<int> GetCustomerCount()
        {
            return await dashboard.GetCustomerCount();
        }

        [HttpGet("GetFeedBacksCount")]
        public async Task<int> GetFeedBacksCount()
        {
            return await dashboard.GetFeedBacksCount();
        }

        [HttpGet("GetProductCount")]
        public async Task<int> GetProductCount()
        {
            return await dashboard.GetProductCount();
        }

        [HttpGet("GetTransactionsCount")]
        public async Task<int> GetTransactionsCount()
        {
            return await dashboard.GetTransactionsCount();
        }
    }
}
