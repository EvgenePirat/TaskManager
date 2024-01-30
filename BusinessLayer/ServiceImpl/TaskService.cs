using AutoMapper;
using BusinessLayer.Models.Enum;
using BusinessLayer.Models.Tasks.Request;
using BusinessLayer.Models.Tasks.Response;
using BusinessLayer.ServiceContract;
using CustomExceptions.AuthorizationExceptions;
using CustomExceptions.CategoryExceptions;
using CustomExceptions.TaskExceptions;
using DataAccessLayer.RepositoryContract;
using Microsoft.Extensions.Logging;

namespace BusinessLayer.ServiceImpl
{
    /// <summary>
    /// Class for implementation methods for Tasks Service
    /// </summary>
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryService _categoryService;
        private readonly ILogger<TaskService> _logger;
        private readonly IMapper _mapper;

        public TaskService(ICategoryService categoryService, ITaskRepository taskRepository, ICategoryRepository categoryRepository, ILogger<TaskService> logger, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _categoryRepository = categoryRepository;
            _logger = logger;
            _mapper = mapper;
            _categoryService = categoryService;
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
                        DataAccessLayer.Entities.Task mappedTask = _mapper.Map<DataAccessLayer.Entities.Task>(taskAddRequest);
                        var result = await _taskRepository.AddTaskAsync(mappedTask);

                        var mappedResult = _mapper.Map<TaskModel>(result);

                        _logger.LogInformation("{service}.{method} - finish add new task in service layer", nameof(TaskService), nameof(AddNewTaskAsync));

                        return mappedResult;
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

        public async Task<TaskModel> ChangeStatusForTask(Status newStatus, Guid taskId)
        {
            _logger.LogInformation("{service}.{method} - start, change status task in service layer", nameof(TaskService), nameof(ChangeStatusForTask));

            var taskFind = _mapper.Map<TaskModel>(await _taskRepository.GetTaskByIdAsync(taskId, true));

            if (taskFind != null)
            {
                taskFind.Status = newStatus;

                var mappedTask = _mapper.Map<DataAccessLayer.Entities.Task>(taskFind);

                var result = await _taskRepository.UpdateTaskAsync(mappedTask);

                _logger.LogInformation("{service}.{method} - finish, change status task in service layer", nameof(TaskService), nameof(ChangeStatusForTask));
                return _mapper.Map<TaskModel>(result);
            }
            else
            {
                _logger.LogError("{service}.{method} - task with id not found", nameof(TaskService), nameof(ChangeStatusForTask));
                throw new TaskArgumentException("Task with id not found");
            }
        }

        public async System.Threading.Tasks.Task DeleteByIdAsync(Guid taskId)
        {
            _logger.LogInformation("{service}.{method} - start delete task in service layer", nameof(TaskService), nameof(DeleteByIdAsync));

            if (await _taskRepository.GetTaskByIdAsync(taskId, true) != null)
            {
                await _taskRepository.DeleteByIdAsync(taskId);

                _logger.LogInformation("{service}.{method} - finish delete task in service layer", nameof(TaskService), nameof(DeleteByIdAsync));
            }
            else
            {
                _logger.LogError("{service}.{method} - not found task for delete", nameof(TaskService), nameof(DeleteByIdAsync));
                throw new TaskArgumentException("task not found for delete");
            }
        }

        public async Task<List<TaskModel>> GetAllTaskForCategoriesAsync(Guid categoryId)
        {
            _logger.LogInformation("{service}.{method} - start get all task for categories in service layer", nameof(TaskService), nameof(GetAllTaskForCategoriesAsync));

            if (await _categoryRepository.GetCategoryByIdAsync(categoryId) != null)
            {
                _logger.LogInformation("{service}.{method} - finish all task for categories in service layer", nameof(TaskService), nameof(GetAllTaskForCategoriesAsync));

                return _mapper.Map<List<TaskModel>>(await _taskRepository.GetAllTasksByCategoryIdAsync(categoryId));
            }
            else
            {
                _logger.LogError("{service}.{method} - category with id not found", nameof(TaskService), nameof(GetAllTaskForCategoriesAsync));
                throw new ArgumentNullException("category with id not found");
            }
        }

        public async Task<TaskModel> GetTaskByIdAsync(Guid taskId, bool include)
        {
            _logger.LogInformation("{service}.{method} - start get task by id in service layer", nameof(TaskService), nameof(GetTaskByIdAsync));

            DataAccessLayer.Entities.Task? task = await _taskRepository.GetTaskByIdAsync(taskId, include);

            if (task != null)
            {
                var mappedTask = _mapper.Map<TaskModel>(task);

                TimeSpan timeUntilFinish = mappedTask.FinishTime - DateTime.Now;

                mappedTask.DaysLeft = timeUntilFinish.Days;

                _logger.LogInformation("{service}.{method} - finish get all task for categories in service layer", nameof(TaskService), nameof(GetTaskByIdAsync));

                return mappedTask;
            }
            else
            {
                _logger.LogError("{service}.{method} - task with id not found", nameof(TaskService), nameof(GetTaskByIdAsync));
                throw new TaskArgumentException("Task with id not found");
            }
        }

        public async Task<TaskModel?> GetTaskByTitleAsync(string titleTask, string? loginUser)
        {
            _logger.LogInformation("{service}.{method} - start, get task by title for user in service layer", nameof(TaskService), nameof(GetTaskByIdAsync));

            if (loginUser == null)
                throw new AuthorizationArgumentException("You need authorization in application");

            var categories = await _categoryService.GetCategoriesForUserAsync(loginUser);

            var listAllTasks = categories.SelectMany(temp => temp.Tasks).ToList();

            var taskResult = listAllTasks.FirstOrDefault(temp => temp.Title == titleTask);

            if(taskResult != null)
            {
                TimeSpan timeUntilFinish = taskResult.FinishTime - DateTime.Now;

                taskResult.DaysLeft = timeUntilFinish.Days;
            }

            _logger.LogInformation("{service}.{method} - finish, get task by title for user in service layer", nameof(TaskService), nameof(GetTaskByIdAsync));

            return taskResult;
        }

        public async Task<TaskModel> UpdateTaskAsync(TaskUpdateModel taskUpdate)
        {
            _logger.LogInformation("{service}.{method} - start update task in service layer", nameof(TaskService), nameof(UpdateTaskAsync));

            if (await _taskRepository.GetTaskByIdAsync(taskUpdate.Id, true) != null)
            {
                var mappedTask = _mapper.Map<DataAccessLayer.Entities.Task>(taskUpdate);
                var result = await _taskRepository.UpdateTaskAsync(mappedTask);

                _logger.LogInformation("{service}.{method} - finish update task in service layer", nameof(TaskService), nameof(UpdateTaskAsync));

                return _mapper.Map<TaskModel>(result);
            }
            else
            {
                _logger.LogError("{service}.{method} - not found task for update", nameof(TaskService), nameof(UpdateTaskAsync));
                throw new TaskArgumentException("task not found for update");
            }
        }
    }
}