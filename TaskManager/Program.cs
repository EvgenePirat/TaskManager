using Autofac;
using Autofac.Extensions.DependencyInjection;
using Serilog;
using TaskManager.Middleware;
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

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            builder.Host.ConfigureContainer<ContainerBuilder>(
                containerBuilder => containerBuilder.RegisterModule(new MappersModules()));

            //for set options for service
            builder.Services.ConfigureService(builder.Configuration);

            var app = builder.Build();
            
            app.UseExceptionHandler("/Error");
            app.UseExceptionHandingMiddleware();
            app.UseSerilogRequestLogging();

            app.UseHsts();
            app.UseHttpsRedirection();

            app.UseSession();

            app.UseHttpLogging();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
