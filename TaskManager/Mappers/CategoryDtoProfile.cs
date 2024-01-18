using AutoMapper;
using BusinessLayer.Models.Categories.Request;
using BusinessLayer.Models.Categories.Response;
using TaskManager.Dto.Categories.Request;
using TaskManager.Dto.Categories.Response;

namespace TaskManager.Mappers
{
    public class CategoryDtoProfile : Profile
    {
        public CategoryDtoProfile()
        {
            CreateMap<CategoryAddDto, CategoryAddModel>();

            CreateMap<CategoryModel, CategoryDto>();
        }
    }
}
