using BusinessLayer.DTO.Request;
using BusinessLayer.DTO.Response;
using BusinessLayer.Mapper;
using BusinessLayer.ServiceContract;
using DataAccessLayer.RepositoryContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ServiceImpl
{
    /// <summary>
    /// Class for implementation methods for Tasks Service
    /// </summary>
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ICategoryRepository _categoryRepository;

        public TaskService(ITaskRepository taskRepository, ICategoryRepository categoryRepository)
        {
            _taskRepository = taskRepository;
            _categoryRepository = categoryRepository;   
        }

        public async Task<TaskResponse?> AddNewTask(TaskAddRequest taskAddRequest)
        {
            if(taskAddRequest != null)
            {
                if(await _categoryRepository.GetCategoryById(taskAddRequest.CategoryId) != null)
                {
                    DataAccessLayer.Entities.Task task = TaskMapper.TaskAddRequestToTask(taskAddRequest);
                    task = await _taskRepository.AddTask(task);
                    return TaskMapper.TaskToTaskResponse(task);
                }
            }
            return null;
        }

        public async Task<List<TaskResponse>?> GetAllTaskForCategories(Guid categoryId)
        {
            if(await _categoryRepository.GetCategoryById(categoryId) != null)
            {
                return (await _taskRepository.GetAllTasks(categoryId)).Select(task => TaskMapper.TaskToTaskResponse(task)).ToList();
            }
            return null;
        }
    }
}