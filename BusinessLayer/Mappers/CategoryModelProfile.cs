using AutoMapper;
using BusinessLayer.Models.Categories.Request;
using BusinessLayer.Models.Categories.Response;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Mappers
{
    public class CategoryModelProfile : Profile
    {
        public CategoryModelProfile()
        {
            CreateMap<CategoryAddModel, Category>();

            CreateMap<Category, CategoryModel>();

            CreateMap<CategoryUpdateModel, Category>();
        }
    }
}
