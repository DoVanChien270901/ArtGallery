using ArtGallery.Application.Catalog.Auctions;
using ArtGallery.Data.Entities;
using ArtGallery.ViewModel.Catalog.Auctions;
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
    public class AuctionsController : ControllerBase
    {
        private readonly IAuctionsService _aucService;
        public AuctionsController(IAuctionsService aucService)
        {
            _aucService = aucService;
        }

        [HttpGet("GetAllAuctions")]
        public Task<IEnumerable<Auction>> GetAllAuctions() 
        {
            return _aucService.GetAllAuctions();
        }
        [HttpPost("InsertAmount")]
        public Task<bool> InsertAmountAuction(InsertAmountInAuctionRequest request) 
        {
            return _aucService.InsertAmount(request);
        }
        [HttpGet("GetAuction/{id}")]
        public Task<Auction> GetAuction(int id)
        {
            return _aucService.GetAuctionById(id);
        }
        [HttpPut("UpdateAuction")]
        public Task<bool> UpdateAuction(UpdateAuctionRequest request)
        {
            return _aucService.UpdateAuction(request);
        }
        [HttpPost("CreateAuction")]
        public Task<bool> CreateAuction(CreateAuctionRequest request)
        {
            return _aucService.CreateAuction(request);
        }
        [HttpDelete("DeleteAuction/{id}")]
        public Task<bool> DeleteAuction(int id)
        {
            return _aucService.DeleteAuction(id);
        }
        [HttpGet("GetWinner/{id}")]
        public Task<ProfileUser> GetWiner(int id)
        {
            return _aucService.WinnerInformation(id);
        }
    }
}
