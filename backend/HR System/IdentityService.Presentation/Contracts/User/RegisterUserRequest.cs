using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Contracts.User
{
    public class RegisterUserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}