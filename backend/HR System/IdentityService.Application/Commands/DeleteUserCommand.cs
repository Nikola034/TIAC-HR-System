using Application.Common.Repositories;
using Common.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Commands
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IAccountRepository _accountRepository;
        public DeleteUserCommandHandler(IAccountRepository clientRepository)
        {
            _accountRepository = clientRepository;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var existingAccount = await _accountRepository.GetUserByIdAsync(request.Id, cancellationToken);
            if (existingAccount is null)
            {
                throw new NotFoundException("Account with that ID doesn't exist!");
            }
            var persistedAccount = await _accountRepository.DeleteAccountAsync(request.Id, cancellationToken);
            return persistedAccount;
        }
    }

    public record DeleteUserCommand(Guid Id) : IRequest<bool>;
}
