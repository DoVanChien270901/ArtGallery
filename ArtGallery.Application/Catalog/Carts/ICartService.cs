using ArtGallery.Data.Entities;
using ArtGallery.ViewModel.Catalog.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Application.Catalog.Carts
{
    public interface ICartService
    {
        public Task<bool> CreateOrder(InsertCart request);
        public Task<List<Order>> GetOrder();
        public Task<bool> UpdateStatus(int id);
        public Task<bool> Delete(int id);
    }
}
