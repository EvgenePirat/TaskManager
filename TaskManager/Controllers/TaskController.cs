using BusinessLayer.DTO.Request;
using BusinessLayer.DTO.Response;
using BusinessLayer.ServiceContract;
using DataAccessLayer.RepositoryContract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using TaskManager.Filteres.ActionFilter.TaskFilteres;
using TaskManager.Filteres.AuthorizationFilter;
using TaskManager.Filteres.ErrorFilteres.TaskErrorFilteres;

namespace TaskManager.Controllers
{
    /// <summary>
    /// controller for working with task logic
    /// </summary>
    [Route("[controller]")]
    [TypeFilter(typeof(AuthorizationFilter))]
    public class TaskController : Controller
    {
        private readonly ICategoryService _categoryService;

        private readonly ITaskService _taskService;

        private readonly ILogger<TaskController> _logger;

        public TaskController(ICategoryService categoryService, ITaskService taskService, ILogger<TaskController> logger)
        {
            _categoryService = categoryService;
            _taskService = taskService;
            _logger = logger;
        }

        /// <summary>
        /// Method for get home page for manager task
        /// </summary>
        /// <returns>returned home page for task logic</returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> Home()
        {
            _logger.LogInformation("{controller}.{method} - Get home page", nameof(TaskController), nameof(Home));

            Guid userId = Guid.Parse(HttpContext.Session.GetString("UserId"));
            List<CategoryResponse> categories = await _categoryService.GetCategoriesForUser(userId);
            return View(categories);
        }

        /// <summary>
        /// Method for get page for add new task in system for user
        /// </summary>
        /// <returns>returned page for add task</returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> AddNewTask()
        {
            _logger.LogInformation("{controller}.{method} - Get add new task page", nameof(TaskController), nameof(AddNewTask));

            string errorMessage = HttpContext.Request.Query["error"].ToString();
            if (errorMessage.Length > 0)
                ViewBag.Errors = new List<string> { errorMessage };

            Guid userId = Guid.Parse(HttpContext.Session.GetString("UserId"));
            List<CategoryResponse> categories = await _categoryService.GetCategoriesForUser(userId);
            ViewBag.Categories = categories.Select(temp => new SelectListItem() { Text = temp.Name, Value = temp.Id.ToString() });
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
        public async Task<IActionResult> AddNewTaskPost(TaskAddRequest taskAddRequest)
        {
            _logger.LogInformation("{controller}.{method} - post task for save", nameof(TaskController), nameof(AddNewTaskPost));

            await _taskService.AddNewTask(taskAddRequest);

            return RedirectToAction("Home", "Task");
        }

        /// <summary>
        /// Method for get task with id
        /// </summary>
        /// <param name="taskId">guid task id for filter</param>
        /// <returns>returned task if finded or exceptions</returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> TaskDetails([Required] Guid taskId)
        {
            _logger.LogInformation("{controller}.{method} - get task details page", nameof(TaskController), nameof(AddNewTaskPost));

            TaskResponse taskResponse = await _taskService.GetTaskWithId(taskId);

            return View(taskResponse);
        }

        public async Task<IActionResult> DeleteTaskPost([Required] Guid taskId)
        {
            _logger.LogInformation("{controller}.{method} - post delete task if find", nameof(TaskController), nameof(AddNewTaskPost));


        }

    }
}
