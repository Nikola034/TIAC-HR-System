using Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.Exceptions
{
    public class NoAvailableDaysOffException : BadRequestException
    {
        public NoAvailableDaysOffException() : base("Provided user has no available days off.")
        {

        }
    }
}
