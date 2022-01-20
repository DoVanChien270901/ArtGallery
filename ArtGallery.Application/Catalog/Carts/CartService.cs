using ArtGallery.Data.EF;
using ArtGallery.Data.Entities;
using ArtGallery.ViewModel.Catalog.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Application.Catalog.Carts
{
    
    public class CartService : ICartService
    {
        private readonly ArtGalleryDbContext _db;
        public CartService(ArtGalleryDbContext db)
        {
            _db = db;
        }
        public async Task<bool> CreateOrder(InsertCart request)
        {
            await _db.Orders.AddAsync(new Order
            { 
                AccountId = request.AccountId,
                Description = request.Description,
                OrderDate = request.OrderDate,
                Total = request.Total,
                Commision = request.Commision,
            });
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var model = _db.Orders.SingleOrDefault(c => c.Id.Equals(id));
            if (model != null)
            {
                _db.Orders.Remove(model);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Order>> GetOrder()
        {
            List<Order> listOrder = _db.Orders.ToList();
            return listOrder;
        }
        public async Task<bool> UpdateStatus(int id)
        {
            var model = _db.Orders.SingleOrDefault(c => c.Id.Equals(id));
            if (model != null)
            {
                model.Status = true;
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
