using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomExceptions.TaskExceptions
{
    public class TaskArgumentException : Exception
    {
        public TaskArgumentException() : base() { }

        public TaskArgumentException(string? message) : base(message) { }

        public TaskArgumentException(string? message, Exception? innerException) : base(message, innerException) { }

    }
}
