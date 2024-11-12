using FastEndpoints;
using MediatR;
using Presentation.Contracts.User;
using Presentation.Mapper;

namespace Presentation.Controllers.User
{
    public class ResetPassword : Endpoint<ResetPasswordRequest>
    {
        IMediator _mediator;
        public ResetPassword(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Post("auth/resetPassword");
            AllowAnonymous();
        }

        public override async Task HandleAsync(ResetPasswordRequest req, CancellationToken ct)
        {
            await _mediator.Send(req.ToCommand(),ct);
            await SendOkAsync("Password successfully changed",ct);
        }
    }
    
}