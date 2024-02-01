using BusinessLayer.ServiceContract;
using CustomExceptions.AuthorizationExceptions;
using DataAccessLayer.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace BusinessLayer.ServiceImpl
{
    /// <summary>
    /// Class implementation remote validation contract
    /// </summary>
    public class RemoteValidationService : IRemoteValidationService
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<RemoteValidationService> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public RemoteValidationService(ICategoryService categoryService, ILogger<RemoteValidationService> logger, SignInManager<ApplicationUser> signInManager)
        {
            _categoryService = categoryService;
            _logger = logger;
            _signInManager = signInManager;
        }

        public async Task<bool?> IsNameTaskAlreadyCreateAsync(string titleTask, string? loginUser)
        {
            _logger.LogInformation("{service}.{method} - start, check exist title task for user in service layer", nameof(RemoteValidationService), nameof(IsNameTaskAlreadyCreateAsync));

            if (loginUser == null)
                throw new AuthorizationArgumentException("You need authorization in application");

            var categories = await _categoryService.GetCategoriesForUserAsync(loginUser);

            var listAllTasks = categories.SelectMany(temp => temp.Tasks).ToList();

            var result = listAllTasks.Any(task => task.Title == titleTask);

            _logger.LogInformation("{service}.{method} - finish, check exist title task for user in service layer", nameof(RemoteValidationService), nameof(IsNameTaskAlreadyCreateAsync));

            return !result;
        }

        public async Task<bool> IsUserNameAlreadyExist(string userName)
        {
            _logger.LogInformation("{service}.{method} - start, check exist username in system in service layer", nameof(RemoteValidationService), nameof(IsNameTaskAlreadyCreateAsync));
            
            var user = await _signInManager.UserManager.FindByNameAsync(userName);
            
            _logger.LogInformation("{service}.{method} - finish, check exist username in system in service layer", nameof(RemoteValidationService), nameof(IsNameTaskAlreadyCreateAsync));

            return user == null;
        }
    }
}
