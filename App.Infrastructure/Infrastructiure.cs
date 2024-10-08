using App.Application.Interfaces;
using App.Domain.Abstractions;
using App.Infrastructure.DataContext;
using App.Infrastructure.Implementation;
using App.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace App.Infrastructure
{
    public static class Infrastructiure
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection srv, IConfiguration cfg) 
        {
            srv.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(cfg.GetConnectionString("dbcs"));
            });
            srv.Configure<IdentityOptions>(options =>
            {
                // Default Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                // Default Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false; // For special character
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                // Default SignIn settings.
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.User.RequireUniqueEmail = true;
            });
            // Add AutoMapper
            srv.AddAutoMapper(typeof(MappingProfile));
            srv.AddScoped<IUowRepo, UowRepo>();
            srv.AddScoped<IEmployeeServices, EmployeeServices>();
            srv.AddScoped<IRoleService, RoleService>();
            srv.AddScoped<IUserRoleService, UserRoleService>();
            srv.AddScoped<IUserService, UserService>();
            return srv; 
        }
    }
}
