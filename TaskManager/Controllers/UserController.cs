using BusinessLayer.DTO.Response;
using BusinessLayer.ServiceContract;
using DataAccessLayer.RepositoryImpl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public async Task<IActionResult> Registration()
        {
            List<RoleResponse> rolesList = await _roleService.GetAllRoles();

            IEnumerable<SelectListItem> selectListItems = rolesList
                .Select(role => new SelectListItem
                {
                    Value = role.Id.ToString(),
                    Text = role.Name
                });

            ViewBag.Roles = selectListItems;

            return View();
        }
    }
}
