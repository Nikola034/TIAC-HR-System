using Application.Common.Mappers;
using Application.Common.Repositories;
using Application.Common.Services;
using Core.Entities;
using Core.Exceptions;
using MediatR;

namespace Application.Commands
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
    {
        private readonly IAccountRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        public ResetPasswordCommandHandler(IAccountRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        { ;
            var existingUser = await _userRepository.FindUserByPasswordResetTokenAsync(request.ResetPasswordToken,cancellationToken);
            if (existingUser is null)
            {
                throw new InvalidPasswordResetTokenException();
            }
            var hashedPassword = await _passwordHasher.HashPasswordAsync(request.Password, cancellationToken);
            await _userRepository.ChangePasswordAsync(existingUser.Email, hashedPassword, cancellationToken);
        }
    }

    public record ResetPasswordCommand(string ResetPasswordToken, string Password) : IRequest;
}