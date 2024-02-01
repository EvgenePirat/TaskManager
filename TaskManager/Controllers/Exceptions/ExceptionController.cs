using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Controllers.Exceptions
{
    public class ExceptionController : Controller
    {
        [Route("/Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            IExceptionHandlerPathFeature? exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exception != null && exception.Error != null)
            {
                ViewBag.ErrorMessage = exception.Error.Message;
            }
            return View();
        }
    }
}
