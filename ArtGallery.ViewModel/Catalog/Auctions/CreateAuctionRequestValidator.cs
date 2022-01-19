using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.ViewModel.Catalog.Auctions
{
    public class CreateAuctionRequestValidator : AbstractValidator<CreateAuctionRequest>
    {
        public CreateAuctionRequestValidator()
        {
            RuleFor(c => c.StartingPrice).NotEqual(0).WithMessage("*Current Price is required");
            RuleFor(c => c.PriceStep).NotEqual(0).WithMessage("*Price Step is required");
            RuleFor(c => c.ProductId).NotEqual(0).WithMessage("*Chose Product in Auction");
            RuleFor(c => c.StartDateTime).LessThan(c => c.EndDateTime).WithMessage("*Start time must be less than end time");
            RuleFor(c => c.StartDateTime).GreaterThan(DateTime.Now).WithMessage("*Start time must be greater than current");
            RuleFor(c => c.EndDateTime).GreaterThan(DateTime.Now).WithMessage("*End time must be greater than current");
            RuleFor(c => c.EndDateTime).GreaterThan(c => c.StartDateTime).WithMessage("*End time must be greater than start time");
        }
    }
}
