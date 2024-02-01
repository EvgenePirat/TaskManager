

using AutoMapper;
using BusinessLayer.Models.Users.Response;
using BusinessLayer.ServiceContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Dto.Enums;
using TaskManager.Dto.Users.Response;
using TaskManager.Filteres.ActionFilter.UserFilters;

namespace TaskManager.Controllers.Mains
{
    /// <summary>
    /// controller for working with user application
    /// </summary>
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    [Authorize(Roles = "User, Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, ILogger<UserController> logger, IMapper mapper)
        {
            _userService = userService;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Method for get page with data about user and for edit user data
        /// </summary>
        /// <returns>returned page for edit user account</returns>
        [HttpGet("[action]")]
        public async Task<ActionResult> UserProfileSetting()
        {
            _logger.LogInformation("{controller}.{method} - Get user profile page, start", nameof(UserController), nameof(UserProfileSetting));

            string errorMessage = HttpContext.Request.Query["error"].ToString();
            if (errorMessage.Length > 0)
                ViewBag.Errors = new List<string> { errorMessage };

            string? userLogin = User.Identity?.Name;

            var model = await _userService.GetUserProfileByLoginAsync(userLogin);

            ViewBag.Cities = Enum.GetNames(typeof(Cities)).ToList();
            ViewBag.Countries = Enum.GetNames(typeof(Countries)).ToList();

            ViewBag.Type = "Manager user";

            var mappedResult = _mapper.Map<UserProfileDto>(model);

            _logger.LogInformation("{controller}.{method} - Get user profile page, finish", nameof(UserController), nameof(UserProfileSetting));

            return View(mappedResult);
        }

        /// <summary>
        /// Method for get page with data about user and for edit user data by user id
        /// </summary>
        /// <param name="userId">guid id for search user</param>
        /// <returns>returned page with data abount find user</returns>
        [HttpGet("[action]")]
        public async Task<ActionResult> UserProfileSettingAdmin(Guid userId)
        {
            _logger.LogInformation("{controller}.{method} - Get user profile page for admin manager, start", nameof(UserController), nameof(UserProfileSetting));

            string errorMessage = HttpContext.Request.Query["error"].ToString();
            if (errorMessage.Length > 0)
                ViewBag.Errors = new List<string> { errorMessage };

            var model = await _userService.GetUserProfileByIdAsync(userId);

            ViewBag.Cities = Enum.GetNames(typeof(Cities)).ToList();
            ViewBag.Countries = Enum.GetNames(typeof(Countries)).ToList();

            ViewBag.Type = "Manager admin";

            var mappedResult = _mapper.Map<UserProfileDto>(model);

            _logger.LogInformation("{controller}.{method} - Get user profile page for admin manager, finish", nameof(UserController), nameof(UserProfileSetting));

            return View("UserProfileSetting", mappedResult);
        }

        /// <summary>
        /// Method for update information about User 
        /// </summary>
        /// <param name="userProfileDto">user with new data for update</param>
        /// <returns>returned user profile page with updated data</returns>
        [HttpPost("[action]")]
        [TypeFilter(typeof(UserValidationActionFilter))]
        public async Task<IActionResult> UserProfileSettingPost(UserProfileDto userProfileDto, string? typeOperation = "userType")
        {
            _logger.LogInformation("{controller}.{method} - Post user profile for update, start", nameof(UserController), nameof(UserProfileSetting));

            if(ViewBag.Errors != null)
            {
                ViewBag.Cities = Enum.GetNames(typeof(Cities)).ToList();
                ViewBag.Countries = Enum.GetNames(typeof(Countries)).ToList();

                return View("UserProfileSetting", userProfileDto);
            }
                
            var mappedModel = _mapper.Map<UserProfileModel>(userProfileDto);

            var modelResult = await _userService.UpdateUserProfileAsync(mappedModel);

            var mappedResult = _mapper.Map<UserProfileDto>(modelResult);

            ViewBag.Cities = Enum.GetNames(typeof(Cities)).ToList();
            ViewBag.Countries = Enum.GetNames(typeof(Countries)).ToList();

            _logger.LogInformation("{controller}.{method} - Post user profile for update, finish", nameof(UserController), nameof(UserProfileSetting));

            if(typeOperation == "adminType")
            {
                ViewBag.Type = "Manager admin";
            }

            return View("UserProfileSetting", mappedResult);
        }

        /// <summary>
        /// Method for get all users in application
        /// </summary>
        /// <returns>returned page with all users for role admin</returns>
        [HttpGet("[action]")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllUsers()
        {
            _logger.LogInformation("{controller}.{method} - get page with all users, start", nameof(UserController), nameof(GetAllUsers));

            var listModels = _userService.GetAllApplicationUsers();

            var mappedResult = _mapper.Map<List<ApplicationUserDto>>(listModels).Where(temp => temp.UserName != User.Identity.Name);

            _logger.LogInformation("{controller}.{method} - get page with all users, finish", nameof(UserController), nameof(GetAllUsers));

            return View("Users", mappedResult);
        }

        /// <summary>
        /// Method for delete user by id
        /// </summary>
        /// <param name="userId">guid user id for find</param>
        /// <returns>returned page without user</returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> DeleteUserById(Guid userId, string? typeOperation = "userDelete")
        {
            _logger.LogInformation("{controller}.{method} -  delete user by id, start", nameof(UserController), nameof(UserProfileSetting));

            await _userService.DeleteUserById(userId);

            _logger.LogInformation("{controller}.{method} -  delete user by id, start", nameof(UserController), nameof(UserProfileSetting));

            if (typeOperation == "adminDelete")
                return RedirectToAction("GetAllUsers", "User");

            return RedirectToAction("Logout", "Account");
        }

    }
}
