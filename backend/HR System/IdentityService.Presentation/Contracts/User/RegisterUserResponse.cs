using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Contracts.User
{
    public class RegisterUserResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
    }
}