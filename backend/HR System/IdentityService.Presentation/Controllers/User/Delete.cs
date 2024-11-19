using FastEndpoints;
using IdentityService.Application.Commands;
using MediatR;

namespace IdentityService.Presentation.Controllers.User
{
    public class Delete : EndpointWithoutRequest<bool>
    {
        private readonly IMediator _mediator;

        public Delete(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Delete("auth/{accountId}");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var projectId = Route<Guid>("accountId");
            var result = await _mediator.Send(new DeleteUserCommand(projectId), ct);
            if (!result)
                await SendNotFoundAsync(ct);
            await SendOkAsync(ct);
        }
    }
}
