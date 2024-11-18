using Application.Common.Repositories;
using Application.Common.Services;
using Core.Exceptions;
using MediatR;

namespace Application.Commands
{
    public class SendPasswordResetEmailCommandHandler : IRequestHandler<SendPasswordResetEmailCommand>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IJwtService _jwtService;
        private readonly IEmailService _emailService;

        public SendPasswordResetEmailCommandHandler(IAccountRepository accountRepository, IJwtService jwtService, IEmailService emailService)
        {
            _accountRepository = accountRepository;
            _jwtService = jwtService;
            _emailService = emailService;
        }

        public async Task Handle(SendPasswordResetEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _accountRepository.GetUserByEmailAsync(request.Email, cancellationToken);
            if (user is null)
            {
                throw new UserDoesNotExistException();
            }
            var token = _jwtService.GenerateByteToken();
            await _emailService.SendPasswordResetEmail(request.Email, token);
            await _accountRepository.UpdatePasswordResetTokenAsync(request.Email, token, cancellationToken);
        }
    }

    public record SendPasswordResetEmailCommand(string Email) : IRequest;
}