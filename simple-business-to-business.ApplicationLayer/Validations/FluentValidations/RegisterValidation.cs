using FluentValidation;
using simple_business_to_business.ApplicationLayer.Modes.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace simple_business_to_business.ApplicationLayer.Validations.FluentValidations
{
    public class RegisterValidation : AbstractValidator<RegisterDTO>
    {
        public RegisterValidation()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Enter a email adress").EmailAddress().WithMessage("Please enter a valid email address");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Please enter a password");

            RuleFor(x => x.PasswordConfirm).Equal(x => x.Password).WithMessage("Password do not match");

            RuleFor(x => x.FullName).NotEmpty().WithMessage("Name can not be empty");

            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName can not be empty.").MinimumLength(3).MaximumLength(20).WithMessage("Minimum 3, maximum 50 character");
        }
    }
}
