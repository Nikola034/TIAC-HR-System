using FastEndpoints;
using IdentityService.Application.Queries;
using IdentityService.Presentation.Contracts.User;
using MediatR;
using Presentation.Mapper;

namespace IdentityService.Presentation.Controllers.User
{
    public class GetAccountsByIds : Endpoint<GetAccountsByIdsRequest, GetAccountsByIdsResponse>
    {
        IMediator _mediator;

        public GetAccountsByIds(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Post("auth/allByIds");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetAccountsByIdsRequest request, CancellationToken ct)
        {
            var accounts = await _mediator.Send(new GetAccountsByIdsQuery(request.Ids));
            if (accounts is null)
            {
                await SendNotFoundAsync(ct);
            }

            await SendOkAsync(accounts.ToApiResponse(), ct);
        }
    }
}
