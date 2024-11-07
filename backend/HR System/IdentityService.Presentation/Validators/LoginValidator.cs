using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using FluentValidation;
using Presentation.Contracts.User;

namespace Presentation.Validators
{
    public class LoginValidator : Validator<LoginUserRequest>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).NotNull()
                                 .NotEmpty()
                                 .EmailAddress()
                                 .WithMessage("Email is required");
            RuleFor(x => x.Password).NotNull()
                                    .NotEmpty()
                                    .WithMessage("Password is required");
        }
    }
}