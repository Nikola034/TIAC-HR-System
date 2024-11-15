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
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenResponse>
    {

        private readonly IAccountRepository _accountRepository;
        private readonly IJwtService _jwtService;
        private readonly IEmployeeHttpClient _employeeHttpClient;

        public RefreshTokenCommandHandler(IAccountRepository accountRepository, IJwtService jwtService, IEmployeeHttpClient employeeHttpClient)
        {
            _accountRepository = accountRepository;
            _jwtService = jwtService;
            _employeeHttpClient = employeeHttpClient;
        }

        public async Task<TokenResponse> Handle(RefreshTokenCommand req, CancellationToken cancellationToken)
        {
            var user = await _accountRepository.FindUserByRefreshTokenAsync(req.RefreshToken, cancellationToken);
            if (user is null)
            {
                throw new InvalidRefreshTokenException();
            }
            
            var responseString = await _employeeHttpClient.GetEmployeeByAccountIdAsync(user.Id,cancellationToken);
            var jsonObj = JObject.Parse(responseString);
            var userRole = jsonObj["role"].ToString();
            var employeeId = jsonObj["id"].ToString();
            var tokens = await _jwtService.GenerateTokensAsync(user.Email,userRole,employeeId);
            await _accountRepository.UpdateRefreshTokenAsync(user.Email, tokens.RefreshToken, cancellationToken);
            return tokens;
        }
    }

    public record RefreshTokenCommand(string RefreshToken) : IRequest<TokenResponse>;
}