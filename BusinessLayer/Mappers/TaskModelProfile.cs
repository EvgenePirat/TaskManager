using AutoMapper;
using BusinessLayer.Models.Tasks.Request;
using BusinessLayer.Models.Tasks.Response;

namespace BusinessLayer.Mappers
{
    public class TaskModelProfile : Profile
    {
        public TaskModelProfile()
        {
            CreateMap<DataAccessLayer.Entities.Task, TaskModel>();

            CreateMap<TaskModel, DataAccessLayer.Entities.Task>().ForMember(model => model.Status, task => task.MapFrom(src => src.Status.ToString()));

            CreateMap<TaskAddModel, DataAccessLayer.Entities.Task>();

            CreateMap<TaskUpdateModel, DataAccessLayer.Entities.Task>();
        }
    }
}
