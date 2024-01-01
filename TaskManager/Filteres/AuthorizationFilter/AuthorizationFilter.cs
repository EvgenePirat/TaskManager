using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TaskManager.Filteres.AuthorizationFilter
{
    /// <summary>
    /// Authorization class for check user in system
    /// </summary>
    public class AuthorizationFilter : IAuthorizationFilter
    {
        /// <summary>
        /// Method check if exist user id in session if not redirect to enter page
        /// </summary>
        /// <param name="context">for get user id from Session</param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(context.HttpContext.Session.GetString("UserId") == null)
            {
                context.Result = new RedirectToActionResult("Enter", "User", new { message = "You need authorization in system" });
            }
        }
    }
}
