using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Windows.Input;
using Application.Common.Mappers;
using Application.Common.Repositories;
using Application.Common.Services;
using Core.Entities;
using Core.Exceptions;
using MediatR;

namespace Application.Commands
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Account>
    {
        private readonly IAccountRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        public RegisterUserCommandHandler(IAccountRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task<Account> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var domainEntity = request.ToDomainEntity();
            var existingUser = await _userRepository.GetUserByEmailAsync(domainEntity.Email);
            if (existingUser is not null)
            {
                throw new UserAlreadyExistException();
            }
            domainEntity.Id = new Guid();
            domainEntity.Password = await _passwordHasher.HashPasswordAsync(request.Password, cancellationToken);
            var persistedUser = await _userRepository.CreateAsync(domainEntity, cancellationToken);
            return persistedUser;
        }

    }

    public record RegisterUserCommand(string Email, string Password) : IRequest<Account>;
}