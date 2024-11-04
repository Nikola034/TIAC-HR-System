using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Exceptions.Handler
{
    public class UnauthorizeException : Exception
    {
        public UnauthorizeException(string message) : base(message)
        {

        }
    }
}