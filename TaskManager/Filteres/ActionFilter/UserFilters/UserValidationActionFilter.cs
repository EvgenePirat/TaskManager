﻿using Microsoft.AspNetCore.Mvc.Filters;
using TaskManager.Controllers.Mains;
using TaskManager.Dto.Users.Response;
using TaskManager.Filteres.ActionFilter.TaskFilteres;

namespace TaskManager.Filteres.ActionFilter.UserFilters
{
    /// <summary>
    /// Class with method for realize logic for validation data before get in controlere
    /// </summary>
    public class UserValidationActionFilter : IAsyncActionFilter
    {
        private readonly ILogger<UserValidationActionFilter> _logger;

        public UserValidationActionFilter(ILogger<UserValidationActionFilter> logger)
        {
            _logger = logger;
        }


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.Controller is UserController userController)
            {
                if (context.HttpContext.Request.Path.Value.Contains("UserProfileSettingPost"))
                {
                    List<string> errorMessages = new List<string>();

                    if (!context.ModelState.IsValid)
                    {
                        _logger.LogError("{filter}.{method} - error with model state for update user", nameof(UserValidationActionFilter), nameof(OnActionExecutionAsync));
                        errorMessages = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

                        userController.ViewBag.Errors = errorMessages;
                        context.Result = userController.View("UserProfileSetting");
                        return;
                    }

                    var formCollection = context.HttpContext.Request.Form;

                    string country = formCollection["Country"];
                    string city = formCollection["City"];
                    bool isShowWeather = formCollection["IsShowWeather"].Contains("true");

                    if((isShowWeather == true) && (country == "Unknown" || city == "Unknown"))
                    {
                        errorMessages.Add("For show weather you need choose country and city");
                        userController.ViewBag.Errors = errorMessages;
                    }
                }
            }

            await next();
        }
    }
}
