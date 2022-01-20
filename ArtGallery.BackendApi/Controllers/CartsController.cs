using ArtGallery.Application.Catalog.Carts;
using ArtGallery.Data.Entities;
using ArtGallery.ViewModel.Catalog.Carts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtGallery.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpPost("Create")]
        public Task<bool> InsertAmountAuction(InsertCart request)
        {
            return _cartService.CreateOrder(request);
        }
        [HttpGet("Update/{id}")]
        public Task<bool> InsertAmountAuction(int id)
        {
            return _cartService.UpdateStatus(id);
        }
        [HttpDelete("Delete/{id}")]
        public Task<bool> Delete(int id)
        {
            return _cartService.Delete(id);
        }
        [HttpGet("Get")]
        public Task<List<Order>> Get()
        {
            return _cartService.GetOrder();
        }
    }
}
