using ArtGallery.Data.Entities;
using ArtGallery.ViewModel.Catalog.Auctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Application.Catalog.Auctions
{
    public interface IAuctionsService
    {
        public Task<IEnumerable<Auction>> GetAllAuctions();
        public Task<Auction> GetAuctionById(int Id);
        public Task<bool> InsertAmount(InsertAmountInAuctionRequest request);
        public Task<bool> DeleteAuction(int Id);
        public Task<bool> UpdateAuction(UpdateAuctionRequest request);
        public Task<ProfileUser> WinnerInformation(int aucId);
        public Task<bool> CreateAuction(CreateAuctionRequest request);
    }
}
