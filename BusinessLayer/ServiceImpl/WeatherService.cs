using BusinessLayer.Models.Users.Response;
using BusinessLayer.Models.Weather.Response;
using BusinessLayer.ServiceContract;
using CustomExceptions.WeatherExceptions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BusinessLayer.ServiceImpl
{
    /// <summary>
    /// Class for implementation contract for weather service
    /// </summary>
    public class WeatherService : IWeatherService
    {
        private readonly ILogger<WeatherService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        private const string API_KEY = "05e03e3e7465f9358779651c398be78a";

        public WeatherService(IHttpClientFactory httpClientFactory, ILogger<WeatherService> logger)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<WeatherModel> GetWeatherForUser(UserWeatherProfileModel userWeatherProfile)
        {
            _logger.LogInformation("{service}.{method} - start, get weather for user", nameof(WeatherService), nameof(GetWeatherForUser));

            string apiUrl = $"http://api.openweathermap.org/data/2.5/weather?q={userWeatherProfile.City},{userWeatherProfile.Country}&appid={API_KEY}&units=metric";

            var httpClient = _httpClientFactory.CreateClient("OpenWeather");

            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string jsonResult = await response.Content.ReadAsStringAsync();

                WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(jsonResult);

                var weatherModel = new WeatherModel()
                {
                    City = weatherResponse.Name,
                    Country = userWeatherProfile.Country,
                    Description = weatherResponse?.Weather?[0].Description,
                    Temperature = weatherResponse?.Main?.Temp              
                };

                weatherModel.Temperature = Math.Round(weatherModel?.Temperature ?? 0);

                _logger.LogInformation("{service}.{method} - finish, get weather for user", nameof(WeatherService), nameof(GetWeatherForUser));

                return weatherModel;
            }
            else
            {
                _logger.LogError("{service}.{method} - error with get information about weather", nameof(WeatherService), nameof(GetWeatherForUser));
                throw new WeatherArgumentException("We can not get information about weather for user");
            }
        }
    }
}
