using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Common.Exceptions;

namespace Core.Exceptions
{
    public class UserAlreadyExistException : BadRequestException
    {
        public UserAlreadyExistException() : base("User with provided email already exists.")
        {

        }

    }
}