using AutoMapper;
using BusinessLayer.Models.Categories.Response;
using BusinessLayer.Models.Tasks.Request;
using BusinessLayer.Models.Tasks.Response;
using BusinessLayer.ServiceContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManager.Dto.Categories.Response;
using TaskManager.Dto.Tasks.Request;
using TaskManager.Dto.Tasks.Response;
using TaskManager.Filteres.ActionFilter.TaskFilteres;
using TaskManager.Filteres.ErrorFilteres.TaskErrorFilteres;

namespace TaskManager.Controllers
{
    /// <summary>
    /// controller for working with task logic
    /// </summary>
    [Route("[controller]")]
    [Authorize(Roles = "User")]
    public class TaskController : Controller
    {
        private readonly ICategoryService _categoryService;

        private readonly ITaskService _taskService;

        private readonly ILogger<TaskController> _logger;

        private readonly IMapper _mapper;

        public TaskController(ICategoryService categoryService, ITaskService taskService, ILogger<TaskController> logger, IMapper mapper)
        {
            _categoryService = categoryService;
            _taskService = taskService;
            _logger = logger;
            _mapper = mapper;
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

            _logger.LogInformation("{controller}.{method} - Get home page, finish", nameof(TaskController), nameof(Home));

            return View(mappedCategories);
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

            ViewBag.Categories = mappedCategories.Select(temp => new SelectListItem() { Text = temp.Name, Value = temp.Id.ToString() });

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
        public async Task<IActionResult> AddNewTaskPost(TaskAddDto taskAddRequest)
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
        public async Task<IActionResult> TaskDetails(Guid taskId)
        {
            _logger.LogInformation("{controller}.{method} - get task details page, start", nameof(TaskController), nameof(AddNewTaskPost));

            TaskModel taskModel = await _taskService.GetTaskByIdAsync(taskId);

            var mappedTask = _mapper.Map<TaskDto>(taskModel);

            _logger.LogInformation("{controller}.{method} - get task details page, finish", nameof(TaskController), nameof(AddNewTaskPost));

            return View(mappedTask);
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

            _logger.LogInformation("{controller}.{method} - finish post delete task if find", nameof(TaskController), nameof(AddNewTaskPost));

            return View("Home", mappedCategories);
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

            var taskToUpdateModel = await _taskService.GetTaskByIdAsync(taskId);

            var mappedTask = _mapper.Map<TaskUpdateDto>(taskToUpdateModel);

            string? userLogin = User.Identity?.Name;

            List<CategoryModel> categories = await _categoryService.GetCategoriesForUserAsync(userLogin);

            var mappedCategories = _mapper.Map<List<CategoryDto>>(categories);

            ViewBag.Categories = mappedCategories.Select(temp => new SelectListItem() { Text = temp.Name, Value = temp.Id.ToString() });

            _logger.LogInformation("{controller}.{method} - get task update page, finish", nameof(TaskController), nameof(TaskUpdate));

            return View(mappedTask);
        }

        /// <summary>
        /// Method for update task
        /// </summary>
        /// <param name="taskUpdate">task with data for update</param>
        /// <returns>returned home page with updates task</returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> TaskUpdatePost([FromForm]TaskUpdateDto taskUpdate)
        {
            _logger.LogInformation("{controller}.{method} - post update task, start", nameof(TaskController), nameof(TaskUpdatePost));

            var mappedModel = _mapper.Map<TaskUpdateModel>(taskUpdate);

            await _taskService.UpdateTaskAsync(mappedModel);

            _logger.LogInformation("{controller}.{method} - post update task, finish", nameof(TaskController), nameof(TaskUpdatePost));

            return RedirectToAction("Home", "Task");
        }

    }
}
