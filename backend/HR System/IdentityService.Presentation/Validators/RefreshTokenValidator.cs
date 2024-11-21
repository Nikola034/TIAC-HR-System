using FastEndpoints;
using FluentValidation;
using Presentation.Contracts.User;

namespace IdentityService.Presentation.Validators;

public class RefreshTokenValidator : Validator<RefreshTokenRequest>
{
    public RefreshTokenValidator()
    {
        RuleFor(x => x.RefreshToken).NotEmpty().NotNull();
    }
}
