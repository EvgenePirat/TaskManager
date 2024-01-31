using AutoMapper;
using BusinessLayer.Models.Enum;
using BusinessLayer.ServiceContract;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Data;
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
                    List<RoleTypes> allRoles = Enum.GetValues(typeof(RoleTypes)).Cast<RoleTypes>().ToList();

                    accountController.ViewBag.Roles = allRoles.ConvertAll(role => new SelectListItem { Value = ((int)role).ToString(), Text = role.ToString() });

                    errorMessages = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                }

                if (context.HttpContext.Request.Path.Value.Contains("Registration"))
                {
                    var formCollection = context.HttpContext.Request.Form;

                    if (formCollection["UserType"] == "Unknown")
                        errorMessages.Add("Please, choose your role");

                    var age = DateTime.Now.Year - DateTime.Parse(formCollection["DateOfBirth"]).Year;

                    if (age < 14 || age > 99)
                        errorMessages.Add("Your age can more 14 but less then 99");
                }

                if(errorMessages.Count > 0)
                {
                    context.Result = accountController.RedirectToAction("Registration", "Account", new { error = errorMessages });
                    return;
                }
                
            }

            await next();
        }
    }
}
