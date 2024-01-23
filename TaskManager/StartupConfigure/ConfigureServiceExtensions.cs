
using BusinessLayer.ServiceContract;
using BusinessLayer.ServiceImpl;
using DataAccessLayer.DbContext;
using DataAccessLayer.IdentityEntities;
using DataAccessLayer.RepositoryContract;
using DataAccessLayer.RepositoryImpl;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.StartupConfigure
{
    /// <summary>
    /// Static class has logic for configure application
    /// </summary>
    public static class ConfigureServiceExtensions
    {
        /// <summary>
        /// Method for set options for IServiceCollection
        /// </summary>
        /// <param name="services">set this for it is class</param>
        /// <param name="configuration">for set option for database</param>
        /// <returns>returned IServiceCollection with all options</returns>
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

            //on http serilog 
            services.AddHttpLogging(options =>
            {
                options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestPropertiesAndHeaders | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                //set options for check password
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequireDigit = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
                .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
