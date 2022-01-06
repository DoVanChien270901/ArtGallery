using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation; 

namespace ArtGallery.ViewModel.Catalog.Products
{
    public class ProductImageValidator : AbstractValidator<ProductImageModelView>
    { 
        public ProductImageValidator()
        {
            RuleFor(c => c.Caption).MaximumLength(150)
                .WithMessage("Caption is maximun length 150");
            RuleFor(c => c.ImagePath).MaximumLength(200)
                .WithMessage("ImagePath is maximum length 200");
            RuleFor(c => c.Thumbnail).MaximumLength(200)
               .WithMessage("Thumbnail is maximum length 200");
        }
    }
}
