using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands;
using Core.Entities;
using Presentation.Contracts.User;

namespace Presentation.Mapper
{
    public static class AccountMapper
    {

        public static RegisterUserCommand ToCommand(this RegisterUserRequest request)
        {
            return new RegisterUserCommand(request.Email, request.Password);
        }

        public static RegisterUserResponse ToApiResponse(this Account account)
        {
            return new RegisterUserResponse
            {
                Id = account.Id,
                Email = account.Email,
            };
        }

        public static LoginUserCommand ToCommand(this LoginUserRequest request) => new LoginUserCommand(request.Email, request.Password);

        public static LoginUserResponse ToApiResponse(this TokenResponse tokenResponse) => new LoginUserResponse(tokenResponse.AccessToken);
    }
}