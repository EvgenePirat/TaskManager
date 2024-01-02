using BusinessLayer.ServiceContract;
using BusinessLayer.ServiceImpl;
using DataAccessLayer;
using DataAccessLayer.RepositoryContract;
using DataAccessLayer.RepositoryImpl;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.StartupConfigure
{
    public static class ConfigureServiceExtensions
    {
        public static IServiceCollection ConfigureService(this IServiceCollection services, IConfiguration configuration)
        {
            //set link for DI
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddControllersWithViews();

            //set string path for dbcontext
            services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"].ToString());
            });

            //set session configuration
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            return services;
        }
    }
}
