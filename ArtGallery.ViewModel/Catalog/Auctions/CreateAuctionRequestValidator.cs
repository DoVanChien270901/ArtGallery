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
            RuleFor(c => c.CurrentPrice).NotNull().WithMessage("Current price is required");
            RuleFor(c => c.PriceStep).NotNull().WithMessage("Price Step is required");
            RuleFor(c => c.NewPrice).NotNull().WithMessage("New price is required")
                .When(c=>c.NewPrice>= (c.CurrentPrice + c.PriceStep)).WithMessage("Please give the amount larger than the minimum price");
        }
    }
}
