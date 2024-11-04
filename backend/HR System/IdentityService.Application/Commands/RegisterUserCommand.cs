using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        public RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task<User> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
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

    public record RegisterUserCommand(string Name, string LastName, string Email, string Password) : IRequest<User>;
}
