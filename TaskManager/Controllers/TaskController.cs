using BusinessLayer.DTO.Request;
using BusinessLayer.DTO.Response;
using BusinessLayer.ServiceContract;
using DataAccessLayer.RepositoryContract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TaskManager.Controllers
{
    /// <summary>
    /// controller for working with task logic
    /// </summary>
    [Route("[controller]")]
    public class TaskController : Controller
    {
        private readonly ICategoryService _categoryService;

        private readonly ITaskService _taskService;

        public TaskController(ICategoryService categoryService, ITaskService taskService)
        {
            _categoryService = categoryService;
            _taskService = taskService;
        }

        /// <summary>
        /// Method for get home page for manager task
        /// </summary>
        /// <returns>returned home page for task logic</returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> Home()
        {
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
        public async Task<IActionResult> AddNewTaskPost(TaskAddRequest taskAddRequest)
        {
            List<string> errorMessages = new List<string>();
            if (!ModelState.IsValid)
            {
                errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

                ViewBag.Errors = errorMessages;
                return View("AddTask");
            }

            TaskResponse response = await _taskService.AddNewTask(taskAddRequest);

            if(response == null)
            {
                errorMessages.Add("Errors with task data check fields");
                Guid userId = Guid.Parse(HttpContext.Session.GetString("UserId"));
                List<CategoryResponse> categories = await _categoryService.GetCategoriesForUser(userId);

                ViewBag.Categories = categories.Select(temp => new SelectListItem() { Text = temp.Name, Value = temp.Id.ToString() });
                ViewBag.Errors = errorMessages;
                return View("AddTask");
            }
            return RedirectToAction("Home", "Task");
        }

    }
}
