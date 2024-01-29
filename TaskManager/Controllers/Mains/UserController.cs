

using AutoMapper;
using BusinessLayer.ServiceContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Dto.Enums;
using TaskManager.Dto.Users.Response;

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

        [HttpGet("[action]")]
        public async Task<ActionResult> UserProfileSetting()
        {
            _logger.LogInformation("{controller}.{method} - Get user profile page, start", nameof(UserController), nameof(UserProfileSetting));

            string? userLogin = User.Identity?.Name;

            var model = await _userService.GetUserProfileAsync(userLogin);

            ViewBag.Cities = Enum.GetNames(typeof(Cities)).ToList();
            ViewBag.Countries = Enum.GetNames(typeof(Countries)).ToList();

            var mappedResult = _mapper.Map<UserProfileDto>(model);

            _logger.LogInformation("{controller}.{method} - Get user profile page, finish", nameof(UserController), nameof(UserProfileSetting));

            return View(mappedResult);
        }

    }
}
