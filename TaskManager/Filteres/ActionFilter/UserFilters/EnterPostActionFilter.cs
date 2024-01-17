using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManager.Controllers;

namespace TaskManager.Filteres.ActionFilter.UserFilters
{
    /// <summary>
    ///  Filter for working with data from user before and after call EnterPost method
    /// </summary>
    public class EnterPostActionFilter : IAsyncActionFilter
    {
        /// <summary>
        /// Method with logic for check ModelState before call EnterPost method
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.Controller is UserController userController)
            {
                List<string> errorMessages = new List<string>();
                if (!context.ModelState.IsValid)
                {
                    errorMessages = userController.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

                    userController.ViewBag.Errors = errorMessages;
                    context.Result = userController.View("Enter");
                }
                else
                    await next();
            }
            else
                await next();
        }
    }
}
