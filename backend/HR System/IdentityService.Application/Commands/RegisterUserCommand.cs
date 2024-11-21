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
        private readonly IEmailService _emailService;
        private readonly IJwtService _jwtService;
        public RegisterUserCommandHandler(IAccountRepository userRepository, IPasswordHasher passwordHasher,
            IEmailService emailService, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
            _jwtService = jwtService;
        }
        public async Task<Account> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(request.Email, cancellationToken);
            if (existingUser is not null)
            {
                throw new UserAlreadyExistException();
            }
            var domainEntity = request.ToDomainEntity();
            domainEntity.Password = await _passwordHasher.HashPasswordAsync(request.Password, cancellationToken);
            var token = _jwtService.GenerateByteToken();
            domainEntity.PasswordResetToken = token;
            await _emailService.SendPasswordResetEmail(request.Email, token);
            var persistedUser = await _userRepository.CreateAsync(domainEntity, cancellationToken);
            return persistedUser;
        }

    }

    public record RegisterUserCommand(string Email, string Password) : IRequest<Account>;
}