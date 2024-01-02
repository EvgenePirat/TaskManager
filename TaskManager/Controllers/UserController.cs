using BusinessLayer.DTO.Request;
using BusinessLayer.DTO.Response;
using BusinessLayer.ServiceContract;
using BusinessLayer.ServiceImpl;
using DataAccessLayer.Entities;
using DataAccessLayer.RepositoryImpl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManager.Filteres.ActionFilter.UserFilters;
using TaskManager.Filteres.ErrorFilteres.UserErrorFilteres;

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
        private readonly ILogger<UserController> _logger;

        public UserController(IRoleService roleService, IUserService userService, ILogger<UserController> logger)
        {
            _roleService = roleService;
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// return html css page for enter in system for get request
        /// </summary>
        /// <returns>return html css page for enter</returns>
        [HttpGet("[action]")]
        [HttpGet("/")]
        public IActionResult Enter(string? message)
        {
            if (message != null)
                ViewBag.Message = message;

            return View();
        }

        /// <summary>
        /// method for post request for enter in system
        /// </summary>
        /// <param name="userEnterRequest">data for enter from user<</param>
        /// <returns>returned redirect or view with error</returns>
        [HttpPost("[action]/enter")]
        [TypeFilter(typeof(EnterPostActionFilter))]
        public async Task<IActionResult> EnterPost(UserEnterRequest userEnterRequest)
        {
            List<string> errorMessages = new List<string>();

            if (await _userService.CheckUserName(userEnterRequest.UserName))
            {
                errorMessages.Add("UserName not found in system!");
                ViewBag.Errors = errorMessages;
                return View("Enter");
            }
            UserResponse? findUser = await _userService.EnterInSystem(userEnterRequest);

            if(findUser == null)
            {
                errorMessages.Add("You wrote wrong password!");
                ViewBag.Errors = errorMessages;
                return View("Enter");
            }

            HttpContext.Session.SetString("UserId", findUser.Id.ToString());
            return RedirectToAction("Home", "Task");
        }


        /// <summary>
        /// return html css page for registration in system for get request
        /// </summary>
        /// <returns>return html css page for registration</returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> Registration()
        {
            string errorMessage = HttpContext.Request.Query["error"].ToString();
            if (errorMessage.Length > 0)
                ViewBag.Errors = new List<string> { errorMessage };

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
        [TypeFilter(typeof(RegistrationActionFilter))]
        [TypeFilter(typeof(UserExceptionFilter))]
        public async Task<IActionResult> RegistrationPost(UserAddRequest userAddRequest)
        {
            if(await _userService.CheckUserName(userAddRequest.UserName))
            {
                UserResponse userResponse = await _userService.AddUser(userAddRequest);
            }
            else
            {
                _logger.LogError("{controller}.{method} - userRequest equals null", nameof(UserController), nameof(RegistrationPost));
                throw new ArgumentException("User with username already exist");
            }

            return RedirectToAction("Enter");
        }
    }
}
