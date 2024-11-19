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
    public class Register : Endpoint<RegisterUserRequest, RegisterUserResponse>
    {
        IMediator _mediator;
        public Register(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Post("auth/register");
            Policies("ManagersOnly");
        }

        public override async Task HandleAsync(RegisterUserRequest req, CancellationToken ct)
        {
            var newUser = await _mediator.Send(req.ToCommand());
            await SendOkAsync(newUser.ToApiResponse());
        }
    }
}