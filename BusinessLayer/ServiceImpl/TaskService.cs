using BusinessLayer.DTO.Request;
using BusinessLayer.DTO.Response;
using BusinessLayer.Mapper;
using BusinessLayer.ServiceContract;
using DataAccessLayer.Entities;
using DataAccessLayer.RepositoryContract;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<TaskService> _logger;

        public TaskService(ITaskRepository taskRepository, ICategoryRepository categoryRepository, ILogger<TaskService> logger)
        {
            _taskRepository = taskRepository;
            _categoryRepository = categoryRepository;   
            _logger = logger;
        }

        public async Task<TaskResponse?> AddNewTask(TaskAddRequest taskAddRequest)
        {
            if(taskAddRequest != null)
            {
                if(await _categoryRepository.GetCategoryById(taskAddRequest.CategoryId) != null)
                {
                    if(taskAddRequest.FinishTime > DateTime.Now)
                    {
                        DataAccessLayer.Entities.Task task = TaskMapper.TaskAddRequestToTask(taskAddRequest);
                        task = await _taskRepository.AddTask(task);
                        return TaskMapper.TaskToTaskResponse(task);
                    }
                    else
                    {
                        _logger.LogError("{service}.{method} - finish time must be more than time now", nameof(TaskService), nameof(AddNewTask));
                        throw new ArgumentException("finish time must be more than time now");
                    }
                }
                else
                {
                    _logger.LogError("{service}.{method} - category not found", nameof(TaskService), nameof(AddNewTask));
                    throw new ArgumentException("category not found");
                }
            }
            else
            {
                _logger.LogError("{service}.{method} - taskAddRequest equals null", nameof(TaskService), nameof(AddNewTask));
                throw new ArgumentNullException("taskAddRequest equals null");
            }
        }

        public async Task<List<TaskResponse>> GetAllTaskForCategories(Guid categoryId)
        {
            if(await _categoryRepository.GetCategoryById(categoryId) != null)
            {
                return (await _taskRepository.GetAllTasks(categoryId)).Select(task => TaskMapper.TaskToTaskResponse(task)).ToList();
            }
            else
            {
                _logger.LogError("{service}.{method} - category with id not found", nameof(TaskService), nameof(GetAllTaskForCategories));
                throw new ArgumentNullException("category with id not found");
            }
        }

        public async Task<TaskResponse> GetTaskWithId(Guid taskId)
        {
            DataAccessLayer.Entities.Task? task = await _taskRepository.GetTaskById(taskId);
            if(task != null)
            {
                return TaskMapper.TaskToTaskResponse(task);
            }
            else
            {
                _logger.LogError("{service}.{method} - task with id not found", nameof(TaskService), nameof(GetTaskWithId));
                throw new ArgumentException("task with id not found");
            }
        }
    }
}