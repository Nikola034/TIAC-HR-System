using Application.Commands;
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
                Name = userCommand.Name,
                LastName = userCommand.LastName,
                Email = userCommand.Email,
                Password = userCommand.Password,
            };
        }

    }
}
