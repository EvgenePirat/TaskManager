using AutoMapper;
using BusinessLayer.Models.Users.Request;
using BusinessLayer.Models.Users.Response;
using DataAccessLayer.Entities;
using DataAccessLayer.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Mappers
{
    public class UserModelProfile : Profile
    {
        public UserModelProfile()
        {
            CreateMap<UserAddModel, User>();

            CreateMap<User, UserModel>();

            CreateMap<UserEnterModel, User>();

            CreateMap<ApplicationUser, UserModel>();

            CreateMap<UserEnterModel, UserModel>();
        }
    }
}
