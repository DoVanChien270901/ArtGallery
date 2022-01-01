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
            RuleFor(c => c.Caption).Length(150)
             .WithMessage("Caption is maximun length 150");
        }
    }
}
