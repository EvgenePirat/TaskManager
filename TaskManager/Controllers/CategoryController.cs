using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Controllers
{
    /// <summary>
    /// Controller with methods for working with category
    /// </summary>
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        /// <summary>
        /// Method for show page for added new category
        /// </summary>
        /// <returns>returned page for added new category</returns>
        [HttpGet("[action]")]
        public IActionResult AddNewCategory()
        {
            return View("AddCategory");
        }
    }
}
