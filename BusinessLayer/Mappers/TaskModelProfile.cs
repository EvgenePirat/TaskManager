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

            CreateMap<TaskAddModel, DataAccessLayer.Entities.Task>();

            CreateMap<TaskUpdateModel, DataAccessLayer.Entities.Task>();
        }
    }
}
