namespace TaskManager.Dto.Users.Response
{
    /// <summary>
    /// Dto implementation logic for work with weather for user in presentation layer
    /// </summary>
    public class UserWeatherProfileDto
    {
        public Guid Id { get; set; }

        public string? Country { get; set; }

        public string? City { get; set; }

        public bool IsShowWeather { get; set; } = false;
    }
}
