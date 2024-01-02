using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace TaskManager.Filteres.ErrorFilteres.UserErrorFilteres
{
    public class UserExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<UserExceptionFilter> _logger;

        public UserExceptionFilter(ILogger<UserExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var errorMessage = context.Exception.Message;
            _logger.LogError("{class}.{method} " + errorMessage, nameof(UserExceptionFilter), nameof(OnException));

            if (context.HttpContext.Request.Path.Value.Contains("RegistrationPost"))
            {
                context.Result = new RedirectToActionResult("Registration", "User", new { error = errorMessage });
            }
            else if (context.HttpContext.Request.Path.Value.Contains("EnterPost"))
            {
                context.Result = new RedirectToActionResult("Enter", "User", new { error = errorMessage });
            }

            context.ExceptionHandled = true;
        }
    }
}
