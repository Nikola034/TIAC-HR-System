using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public class UserAlreadyExistException : BadRequestException
    {
        public UserAlreadyExistException() : base("User with provided email already exists.")
        {

        }

    }
}
