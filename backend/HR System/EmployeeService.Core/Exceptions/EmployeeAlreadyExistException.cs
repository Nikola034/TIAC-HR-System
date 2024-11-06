using Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public class EmployeeAlreadyExistException : BadRequestException
    {
        public EmployeeAlreadyExistException() : base("User with provided email already exists.")
        {

        }

    }
}
