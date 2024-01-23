using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Controllers
{
    public class ExceptionController : Controller
    {
        [Route("/Error")]
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
