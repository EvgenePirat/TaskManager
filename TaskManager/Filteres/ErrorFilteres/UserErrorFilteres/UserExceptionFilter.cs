using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace TaskManager.Filteres.ErrorFilteres.UserErrorFilteres
{
    public class UserExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var errorMessage = context.Exception.Message;
            context.Result = new RedirectToActionResult("Registration", "User", new { error = errorMessage});

            context.ExceptionHandled = true;
        }
    }
}
