using BusinessLayer.Models.Roles.Response;
using BusinessLayer.Models.Users.Response;
using BusinessLayer.ServiceContract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManager.Dto.Users.Request;
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
        public IActionResult Enter()
        {
            _logger.LogInformation("{controller}.{method} - start get enter page", nameof(UserController), nameof(Enter));

            string errorMessage = HttpContext.Request.Query["error"].ToString();
            if (errorMessage.Length > 0)
                ViewBag.Errors = new List<string> { errorMessage };

            _logger.LogInformation("{controller}.{method} - finish get enter page", nameof(UserController), nameof(Enter));

            return View();
        }

        /// <summary>
        /// method for post request for enter in system
        /// </summary>
        /// <param name="userEnterRequest">data for enter from user<</param>
        /// <returns>returned redirect or view with error</returns>
        [HttpPost("[action]/enter")]
        [TypeFilter(typeof(EnterPostActionFilter))]
        [TypeFilter(typeof(UserExceptionFilter))]
        public async Task<IActionResult> EnterPost(UserEnterDto userEnterRequest)
        {
            _logger.LogInformation("{controller}.{method} - start post user for enter in system", nameof(UserController), nameof(EnterPost));

            UserModel findUser = await _userService.EnterInSystemAsync(userEnterRequest);

            HttpContext.Session.SetString("UserId", findUser.Id.ToString());

            _logger.LogInformation("{controller}.{method} - finish post user for enter in system", nameof(UserController), nameof(EnterPost));

            return RedirectToAction("Home", "Task");
        }


        /// <summary>
        /// return html css page for registration in system for get request
        /// </summary>
        /// <returns>return html css page for registration</returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> Registration()
        {
            _logger.LogInformation("{controller}.{method} - start post user for enter in system", nameof(UserController), nameof(Registration));

            string errorMessage = HttpContext.Request.Query["error"].ToString();
            if (errorMessage.Length > 0)
                ViewBag.Errors = new List<string> { errorMessage };

            List<RoleModel> rolesList = await _roleService.GetAllRolesAsync();

            ViewBag.Roles = rolesList.Select(role => new SelectListItem { Value = role.Id.ToString(), Text = role.Name });

            _logger.LogInformation("{controller}.{method} - finish post user for enter in system", nameof(UserController), nameof(Registration));

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
        public async Task<IActionResult> RegistrationPost(UserAddDto userAddRequest)
        {
            _logger.LogInformation("{controller}.{method} - start post user for registration in system", nameof(UserController), nameof(RegistrationPost));

            if (await _userService.CheckUserNameAsync(userAddRequest.UserName))
            {
                UserModel userResponse = await _userService.AddUserAsync(userAddRequest);
            }
            else
            {
                _logger.LogError("{controller}.{method} - userRequest equals null", nameof(UserController), nameof(RegistrationPost));
                throw new ArgumentException("User with username already exist");
            }

            _logger.LogInformation("{controller}.{method} - finish post user for registration in system", nameof(UserController), nameof(RegistrationPost));

            return RedirectToAction("Enter");
        }
    }
}
