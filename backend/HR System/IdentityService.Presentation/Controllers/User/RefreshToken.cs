using Application.Commands;
using FastEndpoints;
using MediatR;
using Presentation.Contracts.User;
using Presentation.Mapper;

namespace Presentation.Controllers.User
{
    public class RefreshToken : Endpoint<RefreshTokenRequest,LoginUserResponse>
    {
        IMediator _mediator;
        public RefreshToken(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Post("auth/refreshToken");
            AllowAnonymous();
        }

        public override async Task HandleAsync(RefreshTokenRequest request, CancellationToken ct)
        {
            var tokens = await _mediator.Send(new RefreshTokenCommand(request.RefreshToken), ct);
            await SendOkAsync(tokens.ToApiResponse(), ct);
        }
    }
    
}