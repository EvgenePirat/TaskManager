using BusinessLayer.DTO.TaskDto.Request;
using BusinessLayer.DTO.TaskDto.Response;
using BusinessLayer.Mapper;
using BusinessLayer.ServiceContract;
using CustomExceptions.TaskExceptions;
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
            _logger.LogInformation("{service}.{method} - start add new task in service layer", nameof(TaskService), nameof(AddNewTask));

            if(taskAddRequest != null)
            {
                if(await _categoryRepository.GetCategoryByIdAsync(taskAddRequest.CategoryId) != null)
                {
                    if(taskAddRequest.FinishTime > DateTime.Now)
                    {
                        DataAccessLayer.Entities.Task task = TaskMapper.TaskAddRequestToTask(taskAddRequest);
                        task = await _taskRepository.AddTask(task);

                        _logger.LogInformation("{service}.{method} - finish add new task in service layer", nameof(TaskService), nameof(AddNewTask));

                        return TaskMapper.TaskToTaskResponse(task);
                    }
                    else
                    {
                        _logger.LogError("{service}.{method} - finish time must be more than time now", nameof(TaskService), nameof(AddNewTask));
                        throw new TaskArgumentException ("finish time must be more than time now");
                    }
                }
                else
                {
                    _logger.LogError("{service}.{method} - category not found", nameof(TaskService), nameof(AddNewTask));
                    throw new TaskArgumentException("category not found");
                }
            }
            else
            {
                _logger.LogError("{service}.{method} - taskAddRequest equals null", nameof(TaskService), nameof(AddNewTask));
                throw new ArgumentNullException("taskAddRequest equals null");
            }
        }

        public async System.Threading.Tasks.Task DeleteWithId(Guid taskId)
        {
            _logger.LogInformation("{service}.{method} - start delete task in service layer", nameof(TaskService), nameof(DeleteWithId));

            if(await _taskRepository.GetTaskById(taskId) != null)
            {
                await _taskRepository.DeleteById(taskId);

                _logger.LogInformation("{service}.{method} - finish delete task in service layer", nameof(TaskService), nameof(DeleteWithId));
            }
            else
            {
                _logger.LogError("{service}.{method} - not found task for delete", nameof(TaskService), nameof(DeleteWithId));
                throw new TaskArgumentException("task not found for delete");
            }
        }

        public async Task<List<TaskResponse>> GetAllTaskForCategories(Guid categoryId)
        {
            _logger.LogInformation("{service}.{method} - start get all task for categories in service layer", nameof(TaskService), nameof(GetAllTaskForCategories));

            if (await _categoryRepository.GetCategoryByIdAsync(categoryId) != null)
            {
                _logger.LogInformation("{service}.{method} - finish all task for categories in service layer", nameof(TaskService), nameof(GetAllTaskForCategories));

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
            _logger.LogInformation("{service}.{method} - start get task by id in service layer", nameof(TaskService), nameof(GetTaskWithId));

            DataAccessLayer.Entities.Task? task = await _taskRepository.GetTaskById(taskId);
            if(task != null)
            {
                _logger.LogInformation("{service}.{method} - finish get all task for categories in service layer", nameof(TaskService), nameof(GetAllTaskForCategories));

                return TaskMapper.TaskToTaskResponse(task);
            }
            else
            {
                _logger.LogError("{service}.{method} - task with id not found", nameof(TaskService), nameof(GetTaskWithId));
                throw new TaskArgumentException("Task with id not found");
            }
        }
    }
}