using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using FluentValidation;
using ProjectServicePresentation.Contracts;

namespace ProjectServicePresentation.Validators
{
    public class CreateClientValidator : Validator<CreateClientRequest>
    {
        public CreateClientValidator()
        {
            RuleFor(client => client.Name).NotEmpty()
                                            .NotNull()
                                            .WithMessage("Name is required.");
            RuleFor(client => client.Country).NotEmpty()
                                            .NotNull()
                                            .WithMessage("Country is required.");
        }
    }
}