using BusinessLayer.ServiceContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Controllers.Validations
{
    /// <summary>
    /// Controller for remote validation any data in application
    /// </summary>
    [Route("[controller]/[action]")]
    [AllowAnonymous]
    public class RemoteValidationController : Controller
    {
        private readonly ILogger<RemoteValidationController> _logger;
        private readonly IRemoteValidationService _remoteValidationService;

        public RemoteValidationController(IRemoteValidationService remoteValidationService, ILogger<RemoteValidationController> logger)
        {
            _logger = logger;
            _remoteValidationService = remoteValidationService;
        }


        /// <summary>
        /// Method for check exist name title 
        /// </summary>
        /// <param name="titleTask"></param>
        /// <returns></returns>
        public async Task<IActionResult> CheckExistNameTask(string title)
        {
            _logger.LogInformation("{controller}.{method} - start, check eixts title for task", nameof(RemoteValidationController), nameof(CheckExistNameTask));

            string? userLogin = User.Identity?.Name;

            var result = await _remoteValidationService.IsNameTaskAlreadyCreateAsync(title, userLogin);

            _logger.LogInformation("{controller}.{method} - finish, check eixts title for task", nameof(RemoteValidationController), nameof(CheckExistNameTask));

            return Json(result);
        }

        /// <summary>
        /// Method for check exist userName in system
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<IActionResult> CheckExistUserName(string userName)
        {
            _logger.LogInformation("{controller}.{method} - start, check eixts username for user", nameof(RemoteValidationController), nameof(CheckExistUserName));

            var result = await _remoteValidationService.IsUserNameAlreadyExist(userName);

            _logger.LogInformation("{controller}.{method} - finish, check eixts username for user", nameof(RemoteValidationController), nameof(CheckExistUserName));

            return Json(result);
        }
    }
}
