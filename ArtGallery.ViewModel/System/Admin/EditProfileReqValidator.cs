using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.ViewModel.System.Admin
{
    public class EditProfileReqValidator : AbstractValidator<EditProfileReq>
    {
        public EditProfileReqValidator()
        {
            RuleFor(p=>p.FullName).NotEmpty().WithMessage("FullName is required");
            RuleFor(p => p.Gender).NotEmpty().WithMessage("Gender is required");
            RuleFor(p => p.Address).NotEmpty().WithMessage("Address is required");

        }
    }
}
