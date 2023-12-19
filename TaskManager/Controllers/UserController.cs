using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        /// <summary>
        /// return html css page for enter in system for get request
        /// </summary>
        /// <returns>return html css page for enter</returns>
        [HttpGet("[action]")]
        [HttpGet("/")]
        public IActionResult Enter()
        {
            return View();
        }

        /// <summary>
        /// return html css page for registration in system for get request
        /// </summary>
        /// <returns>return html css page for registration</returns>
        [HttpGet("[action]")]
        public IActionResult Registration()
        {
            return View();
        }
    }
}
