using ArtGallery.Data.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.ViewModel.System.Admin
{
    class ProfileEditValid : AbstractValidator<ProfileUser>
    {
        public ProfileEditValid()
        {
            //ToProfile
            RuleFor(c => c.FullName).NotEmpty().WithMessage("*Full Name is required")
                    .MaximumLength(200).WithMessage("*Full Name can not over 200 character");
            RuleFor(c => c.Gender).NotEmpty().WithMessage("*Gender is required");
            RuleFor(c => c.Address).NotEmpty().WithMessage("*Address is required");
            RuleFor(c => c.Email).NotEmpty().WithMessage("*Email is required")
                    .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage("*Email format not match");
            RuleFor(c => c.DOB).GreaterThan(DateTime.Now.AddYears(-100)).WithMessage("*Birthday can not greater than 100 years");
            RuleFor(c => c.PhoneNumber).NotEmpty().WithMessage("*Phone number is required")
                   .Matches(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$").WithMessage("*Phone format not match");
        }
    }
}
