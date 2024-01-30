using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.Users.Response
{
    /// <summary>
    /// Model implementation logic for work with weather for user
    /// </summary>
    public class UserWeatherProfileModel
    {
        public Guid Id { get; set; }

        public string? Country { get; set; }

        public string? City { get; set; }

        public bool IsShowWeather { get; set; } = false;
    }
}
