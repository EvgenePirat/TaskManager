using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        [HttpGet("[action]")]
        [HttpGet("/")]
        public IActionResult Enter()
        {
            return View();
        }

        [HttpGet("[action]")]
        public IActionResult Registration()
        {
            return View();
        }
    }
}
