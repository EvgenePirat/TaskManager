
namespace TaskManager.Middleware
{
    public class SessionCheckMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if(context.Request.Path.Value?.Contains("User") == false)
            {
                if(context.Session.GetString("UserId") == null)
                {
                    context.Response.Redirect("~/User/Enter");
                    return;
                }
            }

            await next(context);
        }
    }
}
