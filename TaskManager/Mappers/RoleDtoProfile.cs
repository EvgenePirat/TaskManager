using AutoMapper;
using BusinessLayer.Models.Roles.Response;
using TaskManager.Dto.Roles.Response;

namespace TaskManager.Mappers
{
    public class RoleDtoProfile : Profile
    {
        public RoleDtoProfile()
        {
            CreateMap<RoleModel, RoleDto>();
        }
    }
}
