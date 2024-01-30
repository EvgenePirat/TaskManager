using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.Weather.Response
{
    internal class WeatherResponse
    {
        public List<Weather>? Weather { get; set; }
        public Main? Main { get; set; }
        public string? Name { get; set; }
        public Sys? Sys { get; set; }
    }
}
