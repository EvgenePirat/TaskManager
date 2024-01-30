namespace TaskManager.Dto.Weather.Response
{
    /// <summary>
    /// Dto for output data about weather on site
    /// </summary>
    public class WeatherDto
    {
        public string? Description { get; set; }
        public double? Temperature { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
    }
}
