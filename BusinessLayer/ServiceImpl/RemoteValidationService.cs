using BusinessLayer.ServiceContract;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ServiceImpl
{
    /// <summary>
    /// Class implementation remote validation contract
    /// </summary>
    public class RemoteValidationService : IRemoteValidationService
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<RemoteValidationService> _logger;

        public RemoteValidationService(ICategoryService categoryService, ILogger<RemoteValidationService> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        public async Task<bool?> IsNameTaskAlreadyCreateAsync(string titleTask, string? loginUser)
        {
            _logger.LogInformation("{service}.{method} - start, check exist title task for user in service layer", nameof(RemoteValidationService), nameof(IsNameTaskAlreadyCreateAsync));

            if (loginUser == null)
                throw new ArgumentNullException("You need authorization in application");

            var categories = await _categoryService.GetCategoriesForUserAsync(loginUser);

            var listAllTasks = categories.SelectMany(temp => temp.Tasks).ToList();

            var result = listAllTasks.Any(task => task.Title == titleTask);

            _logger.LogInformation("{service}.{method} - finish, check exist title task for user in service layer", nameof(RemoteValidationService), nameof(IsNameTaskAlreadyCreateAsync));

            return !result;
        }
    }
}
