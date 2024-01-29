using AutoMapper;
using BusinessLayer.Enum;
using BusinessLayer.Models.Users.Request;
using BusinessLayer.ServiceContract;
using CustomExceptions.UserExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManager.Dto.Users.Request;
using TaskManager.Dto.Users.Response;
using TaskManager.Filteres.ActionFilter.UserFilters;
using TaskManager.Filteres.ErrorFilteres.UserErrorFilteres;

namespace TaskManager.Controllers.Authorization
{
    /// <summary>registration and enter in system working with user entites
    /// </summary>
    [Route("[controller]")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;

        public AccountController(IUserService userService, ILogger<AccountController> logger, IMapper mapper)
        {
            _userService = userService;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// return html css page for enter in system for get request
        /// </summary>
        /// <returns>return html css page for enter</returns>
        [HttpGet("[action]")]
        [HttpGet("/")]
        public IActionResult Enter()
        {
            _logger.LogInformation("{controller}.{method} - start get enter page", nameof(AccountController), nameof(Enter));

            string errorMessage = HttpContext.Request.Query["error"].ToString();
            if (errorMessage.Length > 0)
                ViewBag.Errors = new List<string> { errorMessage };

            _logger.LogInformation("{controller}.{method} - finish get enter page", nameof(AccountController), nameof(Enter));

            return View();
        }

        /// <summary>
        /// return html css page for registration in system for get request
        /// </summary>
        /// <returns>return html css page for registration</returns>
        [HttpGet("[action]")]
        public IActionResult Registration()
        {
            _logger.LogInformation("{controller}.{method} - start post user for enter in system", nameof(AccountController), nameof(Registration));

            string errorMessage = HttpContext.Request.Query["error"].ToString();
            if (errorMessage.Length > 0)
                ViewBag.Errors = new List<string> { errorMessage };

            List<UserTypes> allRoles = Enum.GetValues(typeof(UserTypes)).Cast<UserTypes>().ToList();

            ViewBag.Roles = allRoles.ConvertAll(role => new SelectListItem { Value = ((int)role).ToString(), Text = role.ToString() });

            _logger.LogInformation("{controller}.{method} - finish post user for enter in system", nameof(AccountController), nameof(Registration));

            return View();
        }

        /// <summary>
        /// method for post request for registration new user in system
        /// </summary>
        /// <param name="userAddRequest">data for registration from user</param>
        /// <returns>returned redirect or view with error</returns>
        [HttpPost("[action]/save")]
        [TypeFilter(typeof(AccountExceptionFilter))]
        [TypeFilter(typeof(RegistrationActionFilter))]
        public async Task<IActionResult> RegistrationPost(UserAddDto userAddRequest)
        {
            _logger.LogInformation("{controller}.{method} - start post user for registration in system", nameof(AccountController), nameof(RegistrationPost));

            var mappedModel = _mapper.Map<UserAddModel>(userAddRequest);

            var user = _mapper.Map<UserDto>(await _userService.AddUserAsync(mappedModel));

            _logger.LogInformation("{controller}.{method} - finish post user for registration in system", nameof(AccountController), nameof(RegistrationPost));

            return RedirectToAction("Home", "Task");
        }


        /// <summary>
        /// method for post request for enter in system
        /// </summary>
        /// <param name="userEnterRequest">data for enter from user<</param>
        /// <returns>returned redirect or view with error</returns>
        [HttpPost("[action]/enter")]
        [TypeFilter(typeof(AccountExceptionFilter))]
        [TypeFilter(typeof(EnterPostActionFilter))]
        public async Task<IActionResult> EnterPost(UserEnterDto userEnterRequest)
        {
            _logger.LogInformation("{controller}.{method} - start post user for enter in system", nameof(AccountController), nameof(EnterPost));

            var mappedModel = _mapper.Map<UserEnterModel>(userEnterRequest);

            var user = _mapper.Map<UserDto>(await _userService.EnterInSystemAsync(mappedModel));

            _logger.LogInformation("{controller}.{method} - finish post user for enter in system", nameof(AccountController), nameof(EnterPost));

            return RedirectToAction("Home", "Task");
        }
    }
}
