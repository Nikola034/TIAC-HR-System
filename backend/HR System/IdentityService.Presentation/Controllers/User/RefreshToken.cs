using Application.Commands;
using FastEndpoints;
using MediatR;
using Presentation.Contracts.User;
using Presentation.Mapper;

namespace Presentation.Controllers.User
{
    public class RefreshToken : EndpointWithoutRequest<LoginUserResponse>
    {
        IMediator _mediator;
        public RefreshToken(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Post("auth/refreshToken/{refreshToken}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var refreshToken = Route<string>("refreshToken");
            var tokens = await _mediator.Send(new RefreshTokenCommand(refreshToken), ct);
            await SendOkAsync(tokens.ToApiResponse(), ct);
        }
    }
    
}