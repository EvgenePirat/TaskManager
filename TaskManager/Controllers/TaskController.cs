using BusinessLayer.DTO.Response;
using BusinessLayer.ServiceContract;
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

        public TaskController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Method for get home page for manager task
        /// </summary>
        /// <returns>returned home page for task logic</returns>
        [HttpGet("[action]")]
        public IActionResult Home()
        {
            return View();
        }

        /// <summary>
        /// Method for get page for add new task in system for user
        /// </summary>
        /// <returns>returned page for add task</returns>
        public async Task<IActionResult> AddNewTask()
        {
            Guid userId = Guid.Parse(HttpContext.Session.GetString("UserId"));
            List<CategoryResponse> categories = await _categoryService.GetCategoriesForUser(userId);
            ViewBag.Categories = categories.Select(temp => new SelectListItem() { Text = temp.Name, Value = temp.Id.ToString() });
            return View("AddTask");
        }

    }
}
