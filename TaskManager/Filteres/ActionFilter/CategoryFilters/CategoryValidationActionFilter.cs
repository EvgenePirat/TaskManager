using Microsoft.AspNetCore.Mvc.Filters;
using TaskManager.Controllers;

namespace TaskManager.Filteres.ActionFilter.CategoryFilters
{
    /// <summary>
    /// Filter for validation data category from user for category controller
    /// </summary>
    public class CategoryValidationActionFilter : IAsyncActionFilter
    {
        /// <summary>
        /// Method for validation model before controller logic
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns>returned errors if modelstate false</returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.Controller is CategoryController categoryController)
            {
                List<string> errorMessages = new List<string>();

                if (!context.ModelState.IsValid)
                {
                    if (context.HttpContext.Request.Path.Value.Contains("AddNewCategoryPost"))
                    {
                        errorMessages = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

                        categoryController.ViewBag.Errors = errorMessages;
                        context.Result = categoryController.View("AddCategory");
                        return;
                    }
                }
            }
            await next();
        }
    }
}
