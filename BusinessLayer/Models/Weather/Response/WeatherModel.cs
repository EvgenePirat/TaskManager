using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.Weather.Response
{
    /// <summary>
    /// Model for set data about weather and returned how response
    /// </summary>
    public class WeatherModel
    {
        public string? Description { get; set; }
        public double? Temperature { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
    }
}
