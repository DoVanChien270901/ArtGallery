using ArtGallery.ViewModel.System.Users;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.ViewModel.Catalog.Auctions
{
    public class InsertAmountInAuctionRequestValidator : AbstractValidator<InsertAmountInAuctionRequest>
    {
        public InsertAmountInAuctionRequestValidator()
        {
            //RuleFor(c => c.NewPrice).NotEmpty().WithMessage("Please give your price number")
            //    .When(c => c.NewPrice < c.LowestPrice).WithMessage("Please give an amount greater than the lowest amount");
            When(c => c.NewPrice < c.LowestPrice, () =>
            {
                RuleFor(c => c.NewPrice).NotEqual(0)
                .WithMessage("*Please give your price number");
                RuleFor(c=>c.NewPrice).GreaterThan(c=>c.LowestPrice)
                .WithMessage("*Please give an amount greater than the lowest amount");
            });
        }
    }
}
