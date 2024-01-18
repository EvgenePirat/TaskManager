using BusinessLayer.ServiceContract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManager.Filteres.ErrorFilteres.UserErrorFilteres;

namespace TaskManager.Filteres.ErrorFilteres.TaskErrorFilteres
{
    /// <summary>
    /// Class with methods for added logic after get erros from task methods
    /// </summary>
    public class TaskExceptionFilter : IAsyncExceptionFilter
    {
        private readonly ILogger<TaskExceptionFilter> _logger;
        private readonly ICategoryService _categoryService;

        public TaskExceptionFilter(ILogger<TaskExceptionFilter> logger, ICategoryService categoryService)
        {
            _logger = logger;
            _categoryService = categoryService;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            var errorMessage = context.Exception.Message;
            _logger.LogError("{class}.{method} " + errorMessage, nameof(UserExceptionFilter), nameof(OnExceptionAsync));

            if (context.HttpContext.Request.Path.Value.Contains("AddNewTaskPost"))
            {
                context.Result = new RedirectToActionResult("AddNewTaskAsync", "Task", new { error = errorMessage });
            }
        }
    }
}
