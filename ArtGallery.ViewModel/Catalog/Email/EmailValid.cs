using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.ViewModel.Catalog.Email
{
    class EmailValid : AbstractValidator<MailModelView>
    {
        public EmailValid()
        {
            RuleFor(c => c.Email).NotEmpty().WithMessage("Email is required !!")
                .EmailAddress().WithMessage("A valid email address is required !!");
        }
    }
}
