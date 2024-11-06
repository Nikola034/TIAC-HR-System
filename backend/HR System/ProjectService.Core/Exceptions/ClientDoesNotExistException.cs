using Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Exceptions;

namespace Core.Exceptions
{
    public class ClientDoesNotExistException : BadRequestException
    {
        public ClientDoesNotExistException() : base("Client does not exist")
        {

        }

    }
}
