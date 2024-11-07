using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands;
using Core.Entities;

namespace Application.Common.Mappers
{
    public static class UserMapper
    {
        public static Account ToDomainEntity(this RegisterUserCommand userCommand)
        {
            return new Account
            {
                Email = userCommand.Email,
                Password = userCommand.Password,
            };
        }

    }
}