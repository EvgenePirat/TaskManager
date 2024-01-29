using AutoMapper;
using BusinessLayer.Models.Enum;
using BusinessLayer.ServiceContract;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TaskManager.Controllers.Authorization;

namespace TaskManager.Filteres.ActionFilter.UserFilters
{
    /// <summary>
    /// Filter for working with data from user before and after call RegistrationPost method
    /// </summary>
    public class RegistrationActionFilter : IAsyncActionFilter
    {
        private readonly IMapper _mapper;

        public RegistrationActionFilter(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Method with logic for check ModelState before call RegistrationPost method
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.Controller is AccountController accountController)
            {
                List<string> errorMessages = new List<string>();
                if (!context.ModelState.IsValid)
                {
                    List<UserTypes> allRoles = Enum.GetValues(typeof(UserTypes)).Cast<UserTypes>().ToList();

                    accountController.ViewBag.Roles = allRoles.ConvertAll(role => new SelectListItem { Value = ((int)role).ToString(), Text = role.ToString() });

                    errorMessages = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

                    accountController.ViewBag.Errors = errorMessages;
                    context.Result = accountController.View("Registration");
                }
                else
                    await next();
            }
            else
                await next();
        }
    }
}
