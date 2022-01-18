using ArtGallery.Data.Constants;
using ArtGallery.Data.EF;
using ArtGallery.Data.Entities;
using ArtGallery.ViewModel.Catalog.Auctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Application.Catalog.Auctions
{
    public class AuctionService : IAuctionsService
    {
        private readonly ArtGalleryDbContext _db;
        public AuctionService(ArtGalleryDbContext db)
        {
            _db = db;
        }

        // Get All Auctions
        public async Task<IEnumerable<Auction>> GetAllAuctions()
        {
            //decimal maxprice = _db.AmountInAuctions.Max(c => c.NewPrice);
            IEnumerable<Auction> listAuctions = from auc 
                    in _db.Auctions join pro in _db.Products on auc.ProductId equals pro.Id
                    select new Auction
                    {
                        StartingPrice = auc.StartingPrice,
                        Id = auc.Id,
                        StartDateTime = auc.StartDateTime,
                        ProductId = auc.ProductId,
                        PriceStep = auc.PriceStep,
                        EndDateTime = auc.EndDateTime,
                        Product = new Product
                        {
                            Id = pro.Id,
                            Title = pro.Title,
                            Price = pro.Price
                        }
                    };
            //listAuctions = listAuctions.OrderByDescending(c=>c.AmountInAcctions.Max(c=>c.NewPrice)).First();
            return listAuctions;
        }

        // Insert Amount
        public async Task<bool> InsertAmount(InsertAmountInAuctionRequest request)
        {
            await _db.AmountInAuctions.AddAsync(new AmountInAuction 
            {
                NewPrice = request.NewPrice,                                                         
                AuctionId = request.AuctionId,
                AccountId = request.AccountId}
            );
            await _db.SaveChangesAsync();
            return true;
        }

        // Get Auction By Id
        public async Task<Auction> GetAuctionById(int Id)
        {
            if (_db.AmountInAuctions.FirstOrDefault(c => c.AuctionId.Equals(Id)) == null)
            {
                IEnumerable<Auction> auction1 = from auc
                    in _db.Auctions
                       join pro in _db.Products on auc.ProductId equals pro.Id
                       where auc.Id.Equals(Id)
                       select new Auction
                             {
                                 Id = auc.Id,
                                 StartDateTime = auc.StartDateTime,
                                 ProductId = auc.ProductId,
                                 PriceStep = auc.PriceStep,
                                 StartingPrice = auc.StartingPrice,
                                 EndDateTime = auc.EndDateTime,
                                 Product = new Product
                                 {
                                     Id = pro.Id,
                                     Title = pro.Title,
                                     Price = pro.Price
                                 }
                             };
                return auction1.First();
            }
            decimal maxPrice = _db.AmountInAuctions.Where(c=>c.AuctionId.Equals(Id)).Max(c=>c.NewPrice);
            IEnumerable<Auction> auction = from auc
                    in _db.Auctions
                    join pro in _db.Products on auc.ProductId equals pro.Id
                    join amount in _db.AmountInAuctions on auc.Id equals amount.AuctionId
                    where auc.Id.Equals(Id) && amount.NewPrice >= maxPrice
                    select new Auction
                    {
                        Id = auc.Id,
                        StartDateTime = auc.StartDateTime,
                        ProductId = auc.ProductId,
                        PriceStep = auc.PriceStep,
                        EndDateTime = auc.EndDateTime,
                        Product = new Product
                        {
                            Id = pro.Id,
                            Title = pro.Title,
                            Price = pro.Price
                        },
                        AmountInAcctions = new List<AmountInAuction> 
                        {
                            new AmountInAuction
                            {
                                Id = amount.Id,
                                NewPrice = amount.NewPrice
                            }
                        }
                    };
            return auction.First();
        }

        // Delete Auction
        public async Task<bool> DeleteAuction(int Id)
        {
            Auction auc = await _db.Auctions.FindAsync(Id);
            if (auc == null) return false;
            _db.Auctions.Remove(auc);
            await _db.SaveChangesAsync();
            return true;
        }

        // Update Auction
        public async Task<bool> UpdateAuction(UpdateAuctionRequest request)
        {
            Auction auc = await _db.Auctions.FindAsync(request.Id);
            if (auc == null) return false;
            auc.StartingPrice = request.StartingPrice;
            auc.PriceStep = request.PriceStep;
            auc.StartDateTime = request.StartDateTime;
            auc.EndDateTime = request.EndDateTime;
            await _db.SaveChangesAsync();
            return true;
        }

        // WinnerInformation
        public async Task<ProfileUser> WinnerInformation(int aucId)
        {
            decimal maxPrice = _db.AmountInAuctions.Max(c => c.NewPrice);
            string accId = (from aia in _db.AmountInAuctions
                           where aia.AuctionId == aucId && aia.NewPrice == maxPrice
                           select aia.AccountId).Single();
            ProfileUser profileUser = _db.ProfileUsers.SingleOrDefault(c => c.AccountId.Equals(accId));
            return profileUser;
        }
    }
}
