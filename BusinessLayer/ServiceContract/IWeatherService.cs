using BusinessLayer.Models.Users.Response;
using BusinessLayer.Models.Weather.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ServiceContract
{
    /// <summary>
    /// Class with methods for work with weather in application
    /// </summary>
    public interface IWeatherService
    {
        /// <summary>
        /// Method for get information about weather for user with him city and country 
        /// </summary>
        /// <param name="userWeatherProfile">User with data for search infromation about weather</param>
        /// <returns>returned weather model with main data about weather for user</returns>
        public Task<WeatherModel> GetWeatherForUser(UserWeatherProfileModel userWeatherProfile);
    }
}
