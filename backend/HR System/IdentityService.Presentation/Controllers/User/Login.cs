using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using MediatR;
using Presentation.Contracts.User;
using Presentation.Mapper;

namespace Presentation.Controllers.User
{
    public class Login : Endpoint<LoginUserRequest, LoginUserResponse>
    {
        IMediator _mediator;
        public Login(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Post("auth/login");
            AllowAnonymous();
        }

        public override async Task HandleAsync(LoginUserRequest req, CancellationToken ct)
        {
            var tokens = await _mediator.Send(req.ToCommand(), ct);
            await SendOkAsync(tokens.ToApiResponse(), ct);
        }
    }
}