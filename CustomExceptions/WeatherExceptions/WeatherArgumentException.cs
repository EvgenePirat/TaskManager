using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomExceptions.WeatherExceptions
{
    public class WeatherArgumentException : Exception
    {
        public WeatherArgumentException() : base() { }

        public WeatherArgumentException(string? message) : base(message) { }

        public WeatherArgumentException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
