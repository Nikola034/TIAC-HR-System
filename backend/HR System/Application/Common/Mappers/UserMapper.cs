using Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Application.Common.Mappers
{
    public static class UserMapper
    {
        public static User ToDomainEntity(this RegisterUserCommand userCommand)
        {
            return new User
            {
                Username = userCommand.Username,
                Password = userCommand.Password,
            };
        }

    }
}
