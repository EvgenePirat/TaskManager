using AutoMapper;
using BusinessLayer.Models.Weather.Response;
using TaskManager.Dto.Weather.Response;

namespace TaskManager.Mappers
{
    public class WeatherDtoProfile : Profile
    {
        public WeatherDtoProfile()
        {
            CreateMap<WeatherModel, WeatherDto>();
        }
    }
}
