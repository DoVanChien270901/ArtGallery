using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.ViewModel.Catalog.Email
{
    public class ForgotPasswordValid : AbstractValidator<ForgotPasswordModelView>
    {
        public ForgotPasswordValid()
        {
            RuleFor(c => c.UserName).NotEmpty().WithMessage("UserName is required !!");
        }
    }
}
