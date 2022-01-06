using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ArtGallery.ViewModel.Catalog.Products
{
    public class ProductValidator : AbstractValidator<ProductModelView>
    {
        public ProductValidator()
        {
            RuleFor(c => c.Title).NotNull().WithMessage("Title is required !!")
                .MaximumLength(50).WithMessage("Title is maximun length 50");
            RuleFor(c => c.Description).NotNull()
                .WithMessage("Description is required !!");
            RuleFor(c => c.Price).NotNull().WithMessage("Price is required");
        }
    }
}
