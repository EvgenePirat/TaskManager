using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace TaskManager.Filteres.ErrorFilteres.UserErrorFilteres
{
    public class AccountExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<AccountExceptionFilter> _logger;

        public AccountExceptionFilter(ILogger<AccountExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var errorMessage = context.Exception.Message;
            _logger.LogError("{class}.{method} " + errorMessage, nameof(AccountExceptionFilter), nameof(OnException));

            if (context.HttpContext.Request.Path.Value.Contains("RegistrationPost"))
            {
                context.Result = new RedirectToActionResult("Registration", "Account", new { error = errorMessage });
            }
            else if (context.HttpContext.Request.Path.Value.Contains("EnterPost"))
            {
                context.Result = new RedirectToActionResult("Enter", "Account", new { error = errorMessage });
            }

            context.ExceptionHandled = true;
        }
    }
}
