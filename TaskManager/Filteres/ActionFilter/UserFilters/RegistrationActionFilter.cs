using BusinessLayer.DTO.RoleDto.Response;
using BusinessLayer.ServiceContract;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManager.Controllers;

namespace TaskManager.Filteres.ActionFilter.UserFilters
{
    /// <summary>
    /// Filter for working with data from user before and after call RegistrationPost method
    /// </summary>
    public class RegistrationActionFilter : IAsyncActionFilter
    {
        private readonly IRoleService _roleService;

        public RegistrationActionFilter(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// Method with logic for check ModelState before call RegistrationPost method
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
                    List<RoleDto> rolesList = await _roleService.GetAllRolesAsync();
                    userController.ViewBag.Roles = rolesList.Select(role => new SelectListItem { Value = role.Id.ToString(), Text = role.Name });
                    errorMessages = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

                    userController.ViewBag.Errors = errorMessages;
                    context.Result = userController.View("Registration");
                }
                else
                    await next();
            }
            else
                await next();
        }
    }
}
