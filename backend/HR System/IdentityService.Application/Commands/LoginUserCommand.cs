using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Repositories;
using Application.Common.Services;
using Core.Exceptions;
using MediatR;

namespace Application.Commands
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, TokenResponse>
    {

        private readonly IAccountRepository _accountRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;

        public LoginUserCommandHandler(IAccountRepository accountRepository, IPasswordHasher passwordHasher, IJwtService jwtService)
        {
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<TokenResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _accountRepository.GetUserByEmailAsync(request.Email, cancellationToken);
            if (user is null)
            {
                throw new WrongCredentialsException();
            }
            var isPasswordCorrect = await _passwordHasher.VerifyPasswordAsync(request.Password, user.Password, cancellationToken);

            if (!isPasswordCorrect)
            {
                throw new WrongCredentialsException();
            }

            var refreshToken = _jwtService.GenerateByteToken();
            await _accountRepository.UpdateRefreshToken(request.Email, refreshToken, cancellationToken);

            var tokens = await _jwtService.GenerateTokensAsync(user.Email);

            return tokens;
        }
    }

    public record LoginUserCommand(string Email, string Password) : IRequest<TokenResponse>;

    public record TokenResponse(string AccessToken);
}