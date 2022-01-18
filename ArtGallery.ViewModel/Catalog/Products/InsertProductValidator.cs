using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ArtGallery.ViewModel.Catalog.Products
{
    public class InsertProductValidator : AbstractValidator<InsertProductRequest>
    {
        public InsertProductValidator()
        {
            RuleFor(c => c.Title).NotEmpty().WithMessage("*Title is required").MaximumLength(50)
                .WithMessage("*Title is maximun length 50");
            RuleFor(c => c.Description).NotEmpty()
                .WithMessage("*Description is required !!");
            RuleFor(c => c.Price).NotEmpty()
                .WithMessage("*Price is required");
            RuleFor(c => c.Thumbnail).NotEmpty().WithMessage("*lease select thumbnail image");
            RuleFor(c => c.Images).NotEmpty().WithMessage("*Please select image");
        }
    }
}
