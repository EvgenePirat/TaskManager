using AutoMapper;
using BusinessLayer.Models.Tasks.Request;
using BusinessLayer.Models.Tasks.Response;
using TaskManager.Dto.Tasks.Request;
using TaskManager.Dto.Tasks.Response;

namespace TaskManager.Mappers
{
    public class TaskDtoProfile : Profile
    {
        public TaskDtoProfile()
        {
            CreateMap<TaskModel, TaskDto>();

            CreateMap<TaskAddDto, TaskAddModel>();

            CreateMap<TaskModel, TaskUpdateDto>();

            CreateMap<TaskUpdateDto, TaskUpdateModel>();
        }
    }
}
