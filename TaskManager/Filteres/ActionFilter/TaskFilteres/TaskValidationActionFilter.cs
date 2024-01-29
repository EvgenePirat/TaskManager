using Microsoft.AspNetCore.Mvc.Filters;
using TaskManager.Controllers.Mains;

namespace TaskManager.Filteres.ActionFilter.TaskFilteres
{
    public class TaskValidationActionFilter : IAsyncActionFilter
    {
        private readonly ILogger<TaskValidationActionFilter> _logger;

        public TaskValidationActionFilter(ILogger<TaskValidationActionFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(context.Controller is TaskController taskController)
            {
                if (context.HttpContext.Request.Path.Value.Contains("AddNewTaskPost"))
                {
                    List<string> errorMessages = new List<string>();
                    if (!context.ModelState.IsValid)
                    {
                        _logger.LogError("{filter}.{method} - erro with model state for add new task", nameof(TaskValidationActionFilter), nameof(OnActionExecutionAsync));
                        errorMessages = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

                        taskController.ViewBag.Errors = errorMessages;
                        context.Result = taskController.View("AddTask");
                        return;
                    }
                }
            }

            await next();
            
        }
    }
}
