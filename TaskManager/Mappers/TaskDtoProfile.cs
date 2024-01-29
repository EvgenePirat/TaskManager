using AutoMapper;
using BusinessLayer.Models.Enum;
using BusinessLayer.Models.Tasks.Request;
using BusinessLayer.Models.Tasks.Response;
using TaskManager.Dto.Enums;
using TaskManager.Dto.Tasks.Request;
using TaskManager.Dto.Tasks.Response;

namespace TaskManager.Mappers
{
    public class TaskDtoProfile : Profile
    {
        public TaskDtoProfile()
        {
            CreateMap<TaskModel, TaskDto>().ForMember(dto => dto.Status, model => model.MapFrom(src => Enum.Parse<StatusTask>(src.Status.ToString())));

            CreateMap<TaskAddDto, TaskAddModel>().ForMember(model => model.Status, dto => dto.MapFrom(src => Enum.Parse<Status>(src.Status.ToString())));

            CreateMap<TaskModel, TaskUpdateDto>().ForMember(dto => dto.Status, model => model.MapFrom(src => Enum.Parse<StatusTask>(src.Status.ToString())));

            CreateMap<TaskUpdateDto, TaskUpdateModel>().ForMember(model => model.Status, dto => dto.MapFrom(src => Enum.Parse<Status>(src.Status.ToString()))); ;
        }
    }
}
