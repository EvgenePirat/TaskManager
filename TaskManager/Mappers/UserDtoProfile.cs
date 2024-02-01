using AutoMapper;
using BusinessLayer.Models.Users.Request;
using BusinessLayer.Models.Users.Response;
using TaskManager.Dto.Enums;
using TaskManager.Dto.Users.Request;
using TaskManager.Dto.Users.Response;

namespace TaskManager.Mappers
{
    /// <summary>
    /// Registration mappers bettwen presentation layer and business for applicationUser and UserProfile entities
    /// </summary>
    public class UserDtoProfile : Profile
    {
        public UserDtoProfile()
        {
            CreateMap<UserEnterDto, UserEnterModel>();

            CreateMap<UserModel, UserDto>();

            CreateMap<ApplicationUserModel, ApplicationUserDto>();

            CreateMap<UserWeatherProfileModel, UserWeatherProfileDto>();

            CreateMap<UserWeatherProfileDto, UserWeatherProfileModel>();

            CreateMap<UserAddDto, UserAddModel>();

            CreateMap<UserProfileModel, UserProfileDto>()
                .ForMember(dto => dto.Country, opt => opt.MapFrom(src => Enum.Parse<Countries>(src.Country ?? "Unknown")))
                .ForMember(dto => dto.City, opt => opt.MapFrom(src => Enum.Parse<Cities>(src.City ?? "Unknown")));

            CreateMap<UserProfileDto, UserProfileModel>()
                .ForMember(dto => dto.Country, model => model.MapFrom(src => src.Country.ToString()))
                .ForMember(dto => dto.City, model => model.MapFrom(src => src.City.ToString()));
        }
    }
}
