using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace TaskManager.Middleware
{
    /// <summary>
    /// Global exception handler for get errors and redirect to view error
    /// </summary>
    public class ExceptionHandingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandingMiddleware> _logger;

        public ExceptionHandingMiddleware(RequestDelegate next, ILogger<ExceptionHandingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Method for 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception ex)
            {
                if (ex.InnerException != null)
                {
                    _logger.LogError(ex.InnerException.GetType().ToString() + ex.InnerException.Message);
                }
                else
                {
                    _logger.LogError(ex.GetType().ToString() + ex.Message);
                }

                throw;
            }
        }
    }

    /// <summary>
    /// Part Class configuration
    /// </summary>
    public static class ExceptionHandingMiddlewareExtensions
    {
        /// <summary>
        /// Method for registration exception middleware in program
        /// </summary>
        /// <param name="builder">for set middleware</param>
        /// <returns>returned config</returns>
        public static IApplicationBuilder UseExceptionHandingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandingMiddleware>();
        }
    }
}
