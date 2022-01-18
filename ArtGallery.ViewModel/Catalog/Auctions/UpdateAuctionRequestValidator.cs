using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.ViewModel.Catalog.Auctions
{
    public class UpdateAuctionRequestValidator : AbstractValidator<UpdateAuctionRequest>
    {
        public UpdateAuctionRequestValidator()
        {
            RuleFor(c => c.StartingPrice).NotEqual(0).WithMessage("*Please give your price number");
            RuleFor(c => c.PriceStep).NotEqual(0).WithMessage("*Please give your price number");
            RuleFor(c => c.StartDateTime).LessThan(c=>c.EndDateTime).WithMessage("*Start time must be less than end time");
            RuleFor(c => c.EndDateTime).GreaterThan(DateTime.Now).WithMessage("*End time must be greater than current");
            RuleFor(c => c.EndDateTime).GreaterThan(c=>c.StartDateTime).WithMessage("*End time must be greater than start time");
        }
    }
}
