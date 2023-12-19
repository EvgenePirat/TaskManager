using BusinessLayer.ServiceContract;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private IRoleService _roleService;

        public UserController(IRoleService roleService)
        {
            _roleService = roleService;
        }

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
            ViewBag.Roles = _roleService.GetAllRoles();
            return View();
        }
    }
}
