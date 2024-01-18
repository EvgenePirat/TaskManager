using AutoMapper;
using BusinessLayer.Models.Roles.Response;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Mappers
{
    public class RoleModelProfile : Profile
    {
        public RoleModelProfile()
        {
            CreateMap<Role, RoleModel>();
        }
    }
}
