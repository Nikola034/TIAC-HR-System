using Application.Common.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Queries
{
    public class GetAccountsByIdsQueryHandler : IRequestHandler<GetAccountsByIdsQuery, GetAccountsByIdsQueryResponse>
    {

        private readonly IAccountRepository _accountRepository;
        public GetAccountsByIdsQueryHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<GetAccountsByIdsQueryResponse> Handle(GetAccountsByIdsQuery request, CancellationToken cancellationToken)
        {
            List<Guid> ids = new List<Guid>();
            foreach(var id in request.Ids)
            {
                ids.Add(Guid.Parse(id));
            }
            var accounts = await _accountRepository.GetAccountsByIdsAsync(ids, cancellationToken);
            return new GetAccountsByIdsQueryResponse(accounts);
        }
    }


    public record GetAccountsByIdsQuery(IEnumerable<string> Ids) : IRequest<GetAccountsByIdsQueryResponse>;

    public record GetAccountsByIdsQueryResponse(IEnumerable<Core.Entities.Account> Accounts);
}
