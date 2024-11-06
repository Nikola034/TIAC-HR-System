using EmployeeService.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.Entities
{
    public class HolidayRequest
    {
        public Guid Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public HolidayRequestStatus Status { get; set; }
        public Guid SenderId { get; set; }
        public Employee Sender { get; set; }
    }
}
