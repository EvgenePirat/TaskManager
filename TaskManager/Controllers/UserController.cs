using BusinessLayer.DTO.Request;
using BusinessLayer.DTO.Response;
using BusinessLayer.ServiceContract;
using DataAccessLayer.RepositoryImpl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TaskManager.Controllers
{
    /// <summary>
    /// controller for working with user entites
    /// </summary>
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;

        public UserController(IRoleService roleService, IUserService userService)
        {
            _roleService = roleService;
            _userService = userService;
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
        /// method for post request for enter in system
        /// </summary>
        /// <param name="userEnterRequest">data for enter from user<</param>
        /// <returns>returned redirect or view with error</returns>
        [HttpPost("[action]/enter")]
        public async Task<IActionResult> EnterPost(UserEnterRequest userEnterRequest)
        {
            List<string> errorMessages = new List<string>();
            if (!ModelState.IsValid)
            {
                errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

                ViewBag.Errors = errorMessages;
                return RedirectToAction("Enter");
            }

            UserResponse? findUser = await _userService.EnterInSystem(userEnterRequest);

            if(findUser.UserName == null)
            {
                errorMessages.Add("Error with username!");
                ViewBag.Errors = errorMessages;
                return View("Enter");
            }   
            else if(findUser.Password == null)
            {
                errorMessages.Add("Error with username!");
                ViewBag.Errors = errorMessages;
                return RedirectToAction("Enter");
            }
            else
            {
                return RedirectToAction("Home", "Task");
            }
        }


        /// <summary>
        /// return html css page for registration in system for get request
        /// </summary>
        /// <returns>return html css page for registration</returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> Registration()
        {
            List<RoleResponse> rolesList = await _roleService.GetAllRoles();

            ViewBag.Roles = rolesList.Select(role => new SelectListItem { Value = role.Id.ToString(), Text = role.Name });

            return View();
        }

        /// <summary>
        /// method for post request for registration new user in system
        /// </summary>
        /// <param name="userAddRequest">data for registration from user</param>
        /// <returns>returned redirect or view with error</returns>
        [HttpPost("[action]/save")]
        public async Task<IActionResult> RegistrationPost(UserAddRequest userAddRequest)
        {
            if( !ModelState.IsValid )
            {
                List<RoleResponse> rolesList = await _roleService.GetAllRoles();
                ViewBag.Roles = rolesList.Select(role => new SelectListItem { Value = role.Id.ToString(), Text = role.Name });
                List<string> errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

                ViewBag.Errors = errorMessages;
                return RedirectToAction("Registration");
            }

            UserResponse userResponse = await _userService.AddUser(userAddRequest);
            return RedirectToAction("Enter");
        }
    }
}
