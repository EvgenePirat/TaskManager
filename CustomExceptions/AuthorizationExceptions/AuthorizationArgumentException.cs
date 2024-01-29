using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomExceptions.AuthorizationExceptions
{
    public class AuthorizationArgumentException : Exception
    {
        public AuthorizationArgumentException() : base() { }

        public AuthorizationArgumentException(string? message) : base(message) { }

        public AuthorizationArgumentException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
