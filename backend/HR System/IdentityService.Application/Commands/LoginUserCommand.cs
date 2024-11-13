using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Repositories;
using Application.Common.Services;
using Common.HttpCLients;
using Core.Exceptions;
using MediatR;
using Newtonsoft.Json.Linq;

namespace Application.Commands
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, TokenResponse>
    {

        private readonly IAccountRepository _accountRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;
        private readonly IEmployeeHttpClient _employeeHttpClient;

        public LoginUserCommandHandler(IAccountRepository accountRepository, IPasswordHasher passwordHasher, IJwtService jwtService, IEmployeeHttpClient employeeHttpClient)
        {
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
            _employeeHttpClient = employeeHttpClient;
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

            var responseString = await _employeeHttpClient.GetEmployeeByAccountId(user.Id,cancellationToken);
            var jsonObj = JObject.Parse(responseString);
            var userRole = jsonObj["role"].ToString();
            var employeeId = jsonObj["id"].ToString();
            var tokens = await _jwtService.GenerateTokensAsync(user.Email,userRole,employeeId);
            await _accountRepository.UpdateRefreshTokenAsync(request.Email, tokens.RefreshToken, cancellationToken);
            return tokens;
        }
    }

    public record LoginUserCommand(string Email, string Password) : IRequest<TokenResponse>;

    public record TokenResponse(string AccessToken, string RefreshToken);
}