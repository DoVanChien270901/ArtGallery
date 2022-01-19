using ArtGallery.Application.Common;
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
        private readonly IStorageService _storageService;
        private readonly ArtGalleryDbContext _db;
        public AuctionService(ArtGalleryDbContext db, IStorageService storageService)
        {
            _storageService = storageService;
            _db = db;
        }

        // Get All Auctions
        public async Task<IEnumerable<Auction>> GetAllAuctions()
        {
            //decimal maxprice = _db.AmountInAuctions.Max(c => c.NewPrice);
            IEnumerable<Auction> listAuctions = (from auc
                    in _db.Auctions
                                                 join pro in _db.Products on auc.ProductId equals pro.Id
                                                 select new Auction
                                                 {
                                                     StartingPrice = auc.StartingPrice,
                                                     Id = auc.Id,
                                                     StartDateTime = auc.StartDateTime,
                                                     ProductId = auc.ProductId,
                                                     PriceStep = auc.PriceStep,
                                                     EndDateTime = auc.EndDateTime,
                                                     AccountId =auc.AccountId,
                                                     Status = auc.Status,
                                                     Product = new Product
                                                     {
                                                         Id = pro.Id,
                                                         Title = pro.Title,
                                                         Price = pro.Price,
                                                         ProductImages = new List<ProductImage> { }
                                                     }
                                                 }).ToList();
            foreach (var item in listAuctions)
            {
                ProductImage image = _db.ProductImages.FirstOrDefault(c => c.ProductId == item.Product.Id && c.Thumbnail == true);
                image.ImagePath = await _storageService.GetFileUrl(image.ImagePath);
                item.Product.ProductImages.Add(image);
            }
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
                Auction auctions = (from auc
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
                                                         Price = pro.Price,
                                                         ProductImages = new List<ProductImage> { }
                                                     }
                                                 }).FirstOrDefault();
                ProductImage image1 = _db.ProductImages.FirstOrDefault(c => c.ProductId == auctions.ProductId && c.Thumbnail == true);
                image1.ImagePath = await _storageService.GetFileUrl(image1.ImagePath);
                auctions.Product.ProductImages.Add(image1);
                return auctions;
            }

            decimal maxPrice = _db.AmountInAuctions.Where(c=>c.AuctionId.Equals(Id)).Max(c=>c.NewPrice);
            Auction auction = (from auc
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
                                                    Price = pro.Price,
                                                    ProductImages = new List<ProductImage> { }
                                                },
                                                AmountInAcctions = new List<AmountInAuction>
                                                    {
                                                        new AmountInAuction
                                                        {
                                                            Id = amount.Id,
                                                            NewPrice = amount.NewPrice
                                                        }
                                                    }
                                            }).FirstOrDefault();
            ProductImage image = _db.ProductImages.FirstOrDefault(c => c.ProductId == auction.ProductId && c.Thumbnail == true);
            image.ImagePath = await _storageService.GetFileUrl(image.ImagePath);
            auction.Product.ProductImages.Add(image);
            return auction;
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

        //Create
        public async Task<bool> CreateAuction(CreateAuctionRequest request)
        {
            Auction auc = new Auction
            {
                AccountId = request.AccountId,
                ProductId = request.ProductId,
                StartingPrice = request.StartingPrice,
                PriceStep = request.PriceStep,
                StartDateTime = request.StartDateTime,
                EndDateTime = request.EndDateTime
            };
            await _db.Auctions.AddAsync(auc);
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
