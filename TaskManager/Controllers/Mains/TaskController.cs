﻿using AutoMapper;
using BusinessLayer.Models.Categories.Response;
using BusinessLayer.Models.Enum;
using BusinessLayer.Models.Tasks.Request;
using BusinessLayer.Models.Tasks.Response;
using BusinessLayer.Models.Users.Response;
using BusinessLayer.ServiceContract;
using CustomExceptions.TaskExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManager.Dto.Categories.Response;
using TaskManager.Dto.Tasks.Request;
using TaskManager.Dto.Tasks.Response;
using TaskManager.Dto.Users.Response;
using TaskManager.Dto.Weather.Response;
using TaskManager.Filteres.ActionFilter.TaskFilteres;
using TaskManager.Filteres.ErrorFilteres.TaskErrorFilteres;
using TaskManager.Helpers;

namespace TaskManager.Controllers.Mains
{
    /// <summary>
    /// controller for working with task logic
    /// </summary>
    [Route("[controller]")]
    [Authorize(Roles = "User, Admin")]
    public class TaskController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ITaskService _taskService;
        private readonly ILogger<TaskController> _logger;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IWeatherService _weatherService;

        public TaskController(IUserService userService, ICategoryService categoryService, ITaskService taskService, ILogger<TaskController> logger, IMapper mapper, IWeatherService weatherService)
        {
            _categoryService = categoryService;
            _taskService = taskService;
            _logger = logger;
            _mapper = mapper;
            _userService = userService;
            _weatherService = weatherService;
        }

        /// <summary>
        /// Method for get home page for manager task
        /// </summary>
        /// <returns>returned home page for task logic</returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> Home()
        {
            _logger.LogInformation("{controller}.{method} - Get home page, start", nameof(TaskController), nameof(Home));

            string? userLogin = User.Identity?.Name;

            List<CategoryModel> categories = await _categoryService.GetCategoriesForUserAsync(userLogin);

            var mappedCategories = _mapper.Map<List<CategoryDto>>(categories);

            mappedCategories = ColorCategoriesHelper.GenerateColorForCategory(mappedCategories);

            var mappedWeatherUser = _mapper.Map<UserWeatherProfileDto>(await _userService.GetUserWeatherProfileAsync(userLogin));

            if (mappedWeatherUser.IsShowWeather)
            {
                var weatherModel = await _weatherService.GetWeatherForUser(_mapper.Map<UserWeatherProfileModel>(mappedWeatherUser));
                
                var mappedResult = _mapper.Map<WeatherDto>(weatherModel);

                ViewBag.Weather = mappedResult;
            }

            _logger.LogInformation("{controller}.{method} - Get home page, finish", nameof(TaskController), nameof(Home));

            return View(mappedCategories);
        }

        /// <summary>
        /// Method for change status for task
        /// </summary>
        /// <param name="newStatus">new status for set in task</param>
        /// <param name="taskId">task if for search task</param>
        /// <returns>returned home page with update task</returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> ChangeStatusTask(Status newStatus, Guid taskId)
        {
            _logger.LogInformation("{controller}.{method} - start, post change status task if find", nameof(TaskController), nameof(ChangeStatusTask));

            await _taskService.ChangeStatusForTask(newStatus, taskId);

            string? userLogin = User.Identity?.Name;

            List<CategoryModel> categories = await _categoryService.GetCategoriesForUserAsync(userLogin);

            var mappedCategories = _mapper.Map<List<CategoryDto>>(categories);

            mappedCategories = ColorCategoriesHelper.GenerateColorForCategory(mappedCategories);

            var mappedWeatherUser = _mapper.Map<UserWeatherProfileDto>(await _userService.GetUserWeatherProfileAsync(userLogin));

            if (mappedWeatherUser.IsShowWeather)
            {
                var weatherModel = await _weatherService.GetWeatherForUser(_mapper.Map<UserWeatherProfileModel>(mappedWeatherUser));

                var mappedResult = _mapper.Map<WeatherDto>(weatherModel);

                ViewBag.Weather = mappedResult;
            }

            _logger.LogInformation("{controller}.{method} - finish, post change status task if find", nameof(TaskController), nameof(ChangeStatusTask));

            return RedirectToAction("Home", "Task");
        }

        /// <summary>
        /// Method for get page for add new task in system for user
        /// </summary>
        /// <returns>returned page for add task</returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> AddNewTask()
        {
            _logger.LogInformation("{controller}.{method} - Get add new task page, start", nameof(TaskController), nameof(AddNewTask));

            string errorMessage = HttpContext.Request.Query["error"].ToString();
            if (errorMessage.Length > 0)
                ViewBag.Errors = new List<string> { errorMessage };

            string? userLogin = User.Identity?.Name;

            List<CategoryModel> categories = await _categoryService.GetCategoriesForUserAsync(userLogin);

            var mappedCategories = _mapper.Map<List<CategoryDto>>(categories);

            var itemCategories = mappedCategories.Select(temp => new SelectListItem() { Text = temp.Name, Value = temp.Id.ToString() });

            ViewBag.Categories = itemCategories;
            ViewBag.CountCategories = itemCategories.Count();

            _logger.LogInformation("{controller}.{method} - Get add new task page, finish", nameof(TaskController), nameof(AddNewTask));

            return View("AddTask");
        }

        /// <summary>
        /// Method for get task from client and save in bd
        /// </summary>
        /// <param name="taskAddRequest">task data from client</param>
        /// <returns>returned home page if good save or page create task with errors</returns>
        [HttpPost("[action]")]
        [TypeFilter(typeof(TaskValidationActionFilter))]
        [TypeFilter(typeof(TaskExceptionFilter))]
        public async Task<IActionResult> AddNewTaskPost([FromForm] TaskAddDto taskAddRequest)
        {
            _logger.LogInformation("{controller}.{method} - post task for save, start", nameof(TaskController), nameof(AddNewTaskPost));

            var mappedModel = _mapper.Map<TaskAddModel>(taskAddRequest);

            await _taskService.AddNewTaskAsync(mappedModel);

            _logger.LogInformation("{controller}.{method} - post task for save, finish", nameof(TaskController), nameof(AddNewTaskPost));

            return RedirectToAction("Home", "Task");
        }

        /// <summary>
        /// Method for get task by id
        /// </summary>
        /// <param name="taskId">guid task id for filter</param>
        /// <returns>returned task if finded or exceptions</returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> TaskDetailsById(Guid taskId)
        {
            _logger.LogInformation("{controller}.{method} - get task details page by id, start", nameof(TaskController), nameof(TaskDetailsById));

            TaskModel taskModel = await _taskService.GetTaskByIdAsync(taskId, true);

            var mappedTask = _mapper.Map<TaskDto>(taskModel);

            _logger.LogInformation("{controller}.{method} - get task details page by id, finish", nameof(TaskController), nameof(TaskDetailsById));

            return View("TaskDetails", mappedTask);
        }

        /// <summary>
        /// Method for get task by id
        /// </summary>
        /// <param name="taskId">guid task id for filter</param>
        /// <returns>returned task if finded or exceptions</returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> TaskDetailsByTitle(string titleTask)
        {
            _logger.LogInformation("{controller}.{method} - get task details page by title task, start", nameof(TaskController), nameof(TaskDetailsByTitle));

            string? userLogin = User.Identity?.Name;

            TaskModel? taskModel = await _taskService.GetTaskByTitleAsync(titleTask, userLogin);

            //i need add logic if not found task
            if (taskModel == null)
            {
                throw new TaskArgumentException("Task by title not found");
            }
                

            var mappedTask = _mapper.Map<TaskDto>(taskModel);

            _logger.LogInformation("{controller}.{method} - get task details page, finish", nameof(TaskController), nameof(TaskDetailsByTitle));

            return View("TaskDetails",mappedTask);
        }

        /// <summary>
        /// Method in format web api for get task with id
        /// </summary>
        /// <param name="taskId">guid id for search task</param>
        /// <returns>returned json format find task</returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetTaskDetails(Guid taskId)
        {
            _logger.LogInformation("{controller}.{method} - get task details page, start", nameof(TaskController), nameof(GetTaskDetails));

            TaskModel taskModel = await _taskService.GetTaskByIdAsync(taskId, false);

            var mappedTask = _mapper.Map<TaskDto>(taskModel);

            _logger.LogInformation("{controller}.{method} - get task details page, finish", nameof(TaskController), nameof(GetTaskDetails));

            return Json(mappedTask);
        }

        /// <summary>
        /// Method in web api format for change status for task
        /// </summary>
        /// <param name="newStatus"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> ChangeStatusApi([FromQuery] int newStatus, [FromQuery] Guid taskId)
        {
            _logger.LogInformation("{controller}.{method} - start, post change status task if find", nameof(TaskController), nameof(ChangeStatusApi));
            Status status = StatusHelper.GetStatusByCode(newStatus);

            await _taskService.ChangeStatusForTask(status, taskId);

            _logger.LogInformation("{controller}.{method} - finish, post change status task if find", nameof(TaskController), nameof(ChangeStatusApi));

            return Ok();
        }

        /// <summary>
        /// Method for delete task by id
        /// </summary>
        /// <param name="taskId">task</param>
        /// <returns>returned page without task</returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteTaskPost(Guid taskId)
        {
            _logger.LogInformation("{controller}.{method} - start post delete task if find", nameof(TaskController), nameof(AddNewTaskPost));

            await _taskService.DeleteByIdAsync(taskId);

            string? userLogin = User.Identity?.Name;

            List<CategoryModel> categories = await _categoryService.GetCategoriesForUserAsync(userLogin);

            var mappedCategories = _mapper.Map<List<CategoryDto>>(categories);

            mappedCategories = ColorCategoriesHelper.GenerateColorForCategory(mappedCategories);

            var mappedWeatherUser = _mapper.Map<UserWeatherProfileDto>(await _userService.GetUserWeatherProfileAsync(userLogin));

            if (mappedWeatherUser.IsShowWeather)
            {
                var weatherModel = await _weatherService.GetWeatherForUser(_mapper.Map<UserWeatherProfileModel>(mappedWeatherUser));

                var mappedResult = _mapper.Map<WeatherDto>(weatherModel);

                ViewBag.Weather = mappedResult;
            }

            _logger.LogInformation("{controller}.{method} - finish post delete task if find", nameof(TaskController), nameof(AddNewTaskPost));

            return RedirectToAction("Home", "Task");
        }

        /// <summary>
        /// Method for get page for update task
        /// </summary>
        /// <param name="taskUpdate"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> TaskUpdate(Guid taskId)
        {
            _logger.LogInformation("{controller}.{method} - get task update page, start", nameof(TaskController), nameof(TaskUpdate));

            var taskToUpdateModel = await _taskService.GetTaskByIdAsync(taskId, true);

            var mappedTask = _mapper.Map<TaskUpdateDto>(taskToUpdateModel);

            string? userLogin = User.Identity?.Name;

            List<CategoryModel> categories = await _categoryService.GetCategoriesForUserAsync(userLogin);

            var mappedCategories = _mapper.Map<List<CategoryDto>>(categories);

            ViewBag.Categories = mappedCategories.Where(temp => temp.Name != mappedTask.CategoryName).Select(temp => new SelectListItem() { Text = temp.Name, Value = temp.Id.ToString() });

            _logger.LogInformation("{controller}.{method} - get task update page, finish", nameof(TaskController), nameof(TaskUpdate));

            return View(mappedTask);
        }

        /// <summary>
        /// Method for update task
        /// </summary>
        /// <param name="taskUpdate">task with data for update</param>
        /// <returns>returned home page with updates task</returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> TaskUpdatePost([FromForm] TaskUpdateDto taskUpdate)
        {
            _logger.LogInformation("{controller}.{method} - post update task, start", nameof(TaskController), nameof(TaskUpdatePost));

            var mappedModel = _mapper.Map<TaskUpdateModel>(taskUpdate);

            await _taskService.UpdateTaskAsync(mappedModel);

            _logger.LogInformation("{controller}.{method} - post update task, finish", nameof(TaskController), nameof(TaskUpdatePost));

            return RedirectToAction("Home", "Task");
        }

    }
}
