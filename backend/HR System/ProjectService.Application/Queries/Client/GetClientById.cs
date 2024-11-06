using Application.Common.Repositories;
using MediatR;

namespace Application.Queries.Client
{
    public class GetClientByIdHandler : IRequestHandler<GetClientByIdQuery, Core.Entities.Client?>
    {
        private readonly IClientRepository _clientRepository;

        public GetClientByIdHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Core.Entities.Client?> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetClientByIdAsync(request.Id);
            return client;
        }
    }

    public record GetClientByIdQuery(Guid Id) : IRequest<Core.Entities.Client>;
}