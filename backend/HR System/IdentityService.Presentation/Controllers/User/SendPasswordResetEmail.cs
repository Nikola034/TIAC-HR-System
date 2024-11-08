using Application.Commands;
using FastEndpoints;
using MediatR;

namespace Presentation.Controllers.User
{
    public class SendPasswordResetEmail : EndpointWithoutRequest
    {
        IMediator _mediator;
        public SendPasswordResetEmail(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Post("auth/resetPassword/{email}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var email = Route<string>("email");
            await _mediator.Send(new SendPasswordResetEmailCommand(email), ct);
            await SendOkAsync("An email has been sent", ct);
        }
    }
    
}