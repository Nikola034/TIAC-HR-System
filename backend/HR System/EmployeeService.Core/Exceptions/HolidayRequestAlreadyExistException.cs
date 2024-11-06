using Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.Exceptions
{
    public class HolidayRequestAlreadyExistException : BadRequestException
    {
        public HolidayRequestAlreadyExistException() : base("Holiday reques with provided email already exists.")
        {

        }
    }
}
