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
            CreateMap<UserAddModel, UserProfile>();

            CreateMap<UserProfile, UserModel>();

            CreateMap<UserEnterModel, UserProfile>();

            CreateMap<ApplicationUser, UserModel>();

            CreateMap<UserEnterModel, UserModel>();

            CreateMap<UserProfileModel, UserProfile>();

            CreateMap<UserProfileModel, ApplicationUser>();
        }
    }
}
