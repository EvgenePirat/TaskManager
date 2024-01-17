using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomExceptions.CategoryExceptions
{
    public class CategoryArgumentException : Exception
    {
        public CategoryArgumentException() : base() { }

        public CategoryArgumentException(string? message) : base(message) { }

        public CategoryArgumentException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
