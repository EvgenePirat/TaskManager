using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Controllers
{
    /// <summary>
    /// controller for working with task logic
    /// </summary>
    [Route("[controller]")]
    public class TaskController : Controller
    {
        /// <summary>
        /// Method for get home page for manager task
        /// </summary>
        /// <returns>returned home page for task logic</returns>
        [HttpGet("[action]")]
        public IActionResult Home()
        {
            return View();
        }

    }
}
