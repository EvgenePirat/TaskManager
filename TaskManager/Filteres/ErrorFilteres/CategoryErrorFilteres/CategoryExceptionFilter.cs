using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TaskManager.Filteres.ErrorFilteres.UserErrorFilteres;

namespace TaskManager.Filteres.ErrorFilteres.CategoryErrorFilteres
{
    /// <summary>
    /// Class with methods with logic for make if get exception from application for category
    /// </summary>
    public class CategoryExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<CategoryExceptionFilter> _logger;

        public CategoryExceptionFilter(ILogger<CategoryExceptionFilter> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Method added logic for working application after throw exception
        /// </summary>
        /// <param name="context">context with exception</param>
        public void OnException(ExceptionContext context)
        {
            var errorMessage = context.Exception.Message;
            _logger.LogError("{class}.{method} " + errorMessage, nameof(CategoryExceptionFilter), nameof(OnException));

            if (context.HttpContext.Request.Path.Value.Contains("AddNewCategoryPost"))
            {
                context.Result = new RedirectToActionResult("AddNewCategoryAsync", "Category", new { error = errorMessage });
            }

            context.ExceptionHandled = true;
        }
    }
}
