using BusinessLayer.ServiceContract;
using BusinessLayer.ServiceImpl;
using DataAccessLayer;
using DataAccessLayer.RepositoryContract;
using DataAccessLayer.RepositoryImpl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TaskManager.StartupConfigure;

namespace TaskManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //for set options for service
            builder.Services.ConfigureService(builder.Configuration);

            var app = builder.Build();

            app.UseSession();
            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllers();

            app.Run();
        }
    }
}
