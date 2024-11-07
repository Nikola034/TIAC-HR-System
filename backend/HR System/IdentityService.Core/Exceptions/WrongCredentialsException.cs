using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Exceptions;

namespace Core.Exceptions
{
    public class WrongCredentialsException : BadRequestException
    {
        public WrongCredentialsException() : base("Wrong email or password")
        {

        }

    }
}