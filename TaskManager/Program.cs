using BusinessLayer.ServiceContract;
using BusinessLayer.ServiceImpl;
using DataAccessLayer;
using DataAccessLayer.RepositoryContract;
using DataAccessLayer.RepositoryImpl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using TaskManager.StartupConfigure;

namespace TaskManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //set options from configure file for serilog
            builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) =>
            {
                loggerConfiguration.ReadFrom.Configuration(context.Configuration).ReadFrom.Services(services);
            });

            //for set options for service
            builder.Services.ConfigureService(builder.Configuration);

            var app = builder.Build();

            app.UseSerilogRequestLogging();
            app.UseHttpLogging();
            app.UseSession();
            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllers();

            app.Run();
        }
    }
}
