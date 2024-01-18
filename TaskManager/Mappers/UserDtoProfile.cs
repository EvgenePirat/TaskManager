using AutoMapper;
using BusinessLayer.Models.Users.Request;
using BusinessLayer.Models.Users.Response;
using TaskManager.Dto.Users.Request;
using TaskManager.Dto.Users.Response;

namespace TaskManager.Mappers
{
    public class UserDtoProfile : Profile
    {
        public UserDtoProfile()
        {
            CreateMap<UserEnterDto, UserEnterModel>();

            CreateMap<UserModel, UserDto>();

            CreateMap<UserAddDto, UserAddModel>();
        }
    }
}
