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
            RuleFor(c => c.Title).NotNull().WithMessage("Title is required !!").MaximumLength(50)
                .WithMessage("Title is maximun length 50");
            RuleFor(c => c.Description).NotNull()
                .WithMessage("Description is required !!");
            RuleFor(c => c.Price).NotNull()
                .WithMessage("Price is required");
            RuleFor(c => c.Caption).MaximumLength(150).WithMessage("Caption is maximun length 150");
            RuleFor(c => c.ImagePath).MaximumLength(200).WithMessage("ImagePath is maximum length 200");
            RuleFor(c => c.Thumbnail).MaximumLength(200).WithMessage("Thumbnail is maximum length 200");
        }
    }
}
