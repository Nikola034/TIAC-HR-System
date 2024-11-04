using Application.Common.Repositories;
using Application.Common.Services;
using Core.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, TokenResponse>
    {

        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;

        public LoginUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<TokenResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Username, cancellationToken);
            if (user is null)
            {
                throw new WrongCredentialsException();
            }
            var isPasswordCorrect = await _passwordHasher.VerifyPasswordAsync(request.Password, user.Password, cancellationToken);

            if (!isPasswordCorrect)
            {
                throw new WrongCredentialsException();
            }

            var tokens = await _jwtService.GenerateTokensAsync(user.Username);

            return tokens;
        }
    }

    public record LoginUserCommand(string Username, string Password) : IRequest<TokenResponse>;

    public record TokenResponse(string AccessToken);
}
