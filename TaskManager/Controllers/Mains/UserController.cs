

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
    [Authorize(Roles = "User")]
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
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<ActionResult> UserProfileSetting()
        {
            _logger.LogInformation("{controller}.{method} - Get user profile page, start", nameof(UserController), nameof(UserProfileSetting));

            string errorMessage = HttpContext.Request.Query["error"].ToString();
            if (errorMessage.Length > 0)
                ViewBag.Errors = new List<string> { errorMessage };

            string? userLogin = User.Identity?.Name;

            var model = await _userService.GetUserProfileAsync(userLogin);

            ViewBag.Cities = Enum.GetNames(typeof(Cities)).ToList();
            ViewBag.Countries = Enum.GetNames(typeof(Countries)).ToList();

            var mappedResult = _mapper.Map<UserProfileDto>(model);

            _logger.LogInformation("{controller}.{method} - Get user profile page, finish", nameof(UserController), nameof(UserProfileSetting));

            return View(mappedResult);
        }

        /// <summary>
        /// Method for update information about User 
        /// </summary>
        /// <param name="userProfileDto">user with new data for update</param>
        /// <returns>returned user profile page with updated data</returns>
        [HttpPost("[action]")]
        [TypeFilter(typeof(UserValidationActionFilter))]
        public async Task<IActionResult> UserProfileSettingPost(UserProfileDto userProfileDto)
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

            return View("UserProfileSetting", mappedResult);
        }

    }
}
