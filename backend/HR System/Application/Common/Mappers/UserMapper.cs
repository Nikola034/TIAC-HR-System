using Application.Commands;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Mappers
{
    public static class UserMapper
    {
        public static User ToDomainEntity(this RegisterUserCommand userCommand)
        {
            return new User
            {
                Username = userCommand.Name,
                Password = userCommand.LastName,
                PasswordResetToken = userCommand.Email,
                RefreshToken = userCommand.Password,
            };
        }

    }
}
