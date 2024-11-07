using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Contracts.User
{
    public class LoginUserResponse
    {
        public string AccessToken { get; set; }

        public LoginUserResponse()
        {

        }

        public LoginUserResponse(string accessToken)
        {
            AccessToken = accessToken;
        }
    }
}