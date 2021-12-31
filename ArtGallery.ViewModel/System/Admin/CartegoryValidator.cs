using ArtGallery.Data.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.ViewModel.System.Admin
{
    class CartegoryValidator : AbstractValidator<CategoryModelView>
    {
        public CartegoryValidator()
        {
            RuleFor(c => c.Name).NotNull()
                .WithMessage("Name is requred !!")
                .Length(50)
                .WithMessage("Name is maximun length 50");
        }
    }
}
