using Application.Common.Repositories;
using Common.Exceptions;
using Core.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Commands
{
    public class BlockUserCommandHandler : IRequestHandler<BlockUserCommand>
    {
        private readonly IAccountRepository _userRepository;
        public BlockUserCommandHandler(IAccountRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task Handle(BlockUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(request.email, cancellationToken);
            if (existingUser is null)
            {
                throw new NotFoundException("Account with provided email doesn't exits!");
            }
            await _userRepository.BlockUnblockUser(existingUser.Email, cancellationToken);
        }
    }

    public record BlockUserCommand(string email) : IRequest;
}
