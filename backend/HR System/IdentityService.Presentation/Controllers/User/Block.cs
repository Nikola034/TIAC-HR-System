using FastEndpoints;
using IdentityService.Presentation.Contracts.User;
using MediatR;
using Presentation.Mapper;

namespace IdentityService.Presentation.Controllers.User
{
    public class Block : Endpoint<BlockUserRequest>
    {
        IMediator _mediator;
        public Block(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Put("auth/block/{email}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(BlockUserRequest req, CancellationToken ct)
        {
            var email = Route<string>("email");
            await _mediator.Send(req.ToCommand(), ct);
            await SendOkAsync("Password successfully changed", ct);
        }
    }
}
