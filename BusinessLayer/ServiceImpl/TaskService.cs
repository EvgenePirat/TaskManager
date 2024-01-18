using BusinessLayer.DTO.TaskDto.Request;
using BusinessLayer.DTO.TaskDto.Response;
using BusinessLayer.Mapper;
using BusinessLayer.Models.Tasks.Request;
using BusinessLayer.Models.Tasks.Response;
using BusinessLayer.ServiceContract;
using CustomExceptions.CategoryExceptions;
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

        public async Task<TaskModel?> AddNewTaskAsync(TaskAddModel taskAddRequest)
        {
            _logger.LogInformation("{service}.{method} - start add new task in service layer", nameof(TaskService), nameof(AddNewTaskAsync));

            if (taskAddRequest != null)
            {
                if (await _categoryRepository.GetCategoryByIdAsync(taskAddRequest.CategoryId) != null)
                {
                    if (taskAddRequest.FinishTime > DateTime.Now)
                    {
                        DataAccessLayer.Entities.Task task = TaskMapper.TaskAddRequestToTask(taskAddRequest);
                        task = await _taskRepository.AddTaskAsync(task);

                        _logger.LogInformation("{service}.{method} - finish add new task in service layer", nameof(TaskService), nameof(AddNewTaskAsync));

                        return TaskMapper.TaskToTaskResponse(task);
                    }
                    else
                    {
                        _logger.LogError("{service}.{method} - finish time must be more than time now", nameof(TaskService), nameof(AddNewTaskAsync));
                        throw new TaskArgumentException("finish time must be more than time now");
                    }
                }
                else
                {
                    _logger.LogError("{service}.{method} - category not found", nameof(TaskService), nameof(AddNewTaskAsync));
                    throw new CategoryArgumentException("category not found");
                }
            }
            else
            {
                _logger.LogError("{service}.{method} - taskAddRequest equals null", nameof(TaskService), nameof(AddNewTaskAsync));
                throw new ArgumentNullException("taskAddRequest equals null");
            }
        }

        public async System.Threading.Tasks.Task DeleteWithIdAsync(Guid taskId)
        {
            _logger.LogInformation("{service}.{method} - start delete task in service layer", nameof(TaskService), nameof(DeleteWithIdAsync));

            if (await _taskRepository.GetTaskByIdAsync(taskId) != null)
            {
                await _taskRepository.DeleteByIdAsync(taskId);

                _logger.LogInformation("{service}.{method} - finish delete task in service layer", nameof(TaskService), nameof(DeleteWithIdAsync));
            }
            else
            {
                _logger.LogError("{service}.{method} - not found task for delete", nameof(TaskService), nameof(DeleteWithIdAsync));
                throw new TaskArgumentException("task not found for delete");
            }
        }

        public async Task<List<TaskModel>> GetAllTaskForCategoriesAsync(Guid categoryId)
        {
            _logger.LogInformation("{service}.{method} - start get all task for categories in service layer", nameof(TaskService), nameof(GetAllTaskForCategoriesAsync));

            if (await _categoryRepository.GetCategoryByIdAsync(categoryId) != null)
            {
                _logger.LogInformation("{service}.{method} - finish all task for categories in service layer", nameof(TaskService), nameof(GetAllTaskForCategoriesAsync));

                return (await _taskRepository.GetAllTasksAsync(categoryId)).Select(task => TaskMapper.TaskToTaskResponse(task)).ToList();
            }
            else
            {
                _logger.LogError("{service}.{method} - category with id not found", nameof(TaskService), nameof(GetAllTaskForCategoriesAsync));
                throw new ArgumentNullException("category with id not found");
            }
        }

        public async Task<TaskModel> GetTaskWithIdAsync(Guid taskId)
        {
            _logger.LogInformation("{service}.{method} - start get task by id in service layer", nameof(TaskService), nameof(GetTaskWithIdAsync));

            DataAccessLayer.Entities.Task? task = await _taskRepository.GetTaskByIdAsync(taskId);
            if (task != null)
            {
                _logger.LogInformation("{service}.{method} - finish get all task for categories in service layer", nameof(TaskService), nameof(GetTaskWithIdAsync));

                return TaskMapper.TaskToTaskResponse(task);
            }
            else
            {
                _logger.LogError("{service}.{method} - task with id not found", nameof(TaskService), nameof(GetTaskWithIdAsync));
                throw new TaskArgumentException("Task with id not found");
            }
        }

        public async Task<TaskModel> UpdateTaskAsync(TaskUpdateModel taskUpdate)
        {
            _logger.LogInformation("{service}.{method} - start update task in service layer", nameof(TaskService), nameof(UpdateTaskAsync));

            if (await _taskRepository.GetTaskByIdAsync(taskUpdate.Id) != null)
            {
                var mappedTask = TaskMapper.TaskUpdateRequestToTask(taskUpdate);
                var result = await _taskRepository.UpdateTaskAsync(mappedTask);
                return TaskMapper.TaskToTaskResponse(result);
            }
            else
            {
                _logger.LogError("{service}.{method} - not found task for update", nameof(TaskService), nameof(UpdateTaskAsync));
                throw new TaskArgumentException("task not found for update");
            }
        }
    }
}