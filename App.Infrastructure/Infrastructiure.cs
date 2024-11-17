using App.Application.Interfaces;
using App.Domain.Abstractions;
using App.Infrastructure.DataContext;
using App.Infrastructure.Implementation;
using App.Infrastructure.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.WebEncoders;
using System.Text.Encodings.Web;
using System.Text.Unicode;


namespace App.Infrastructure
{
    public static class Infrastructiure
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection _sc_services, IConfiguration cfg) 
        {
            _sc_services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(cfg.GetConnectionString("dbcs"));
            });
            _sc_services.Configure<WebEncoderOptions>(options =>
            {
                options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
            });

            _sc_services.AddTransient<DapperDbContext>();
            _sc_services.AddHttpContextAccessor();
            _sc_services.Configure<IdentityOptions>(options =>
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
            _sc_services.AddAutoMapper(typeof(MappingProfile));
            _sc_services.AddScoped<IUowRepo, UowRepo>();
            _sc_services.AddScoped<IEmployeeServices, EmployeeServices>();
            _sc_services.AddScoped<IRoleService, RoleService>();
            _sc_services.AddScoped<IUserRoleService, UserRoleService>();
            _sc_services.AddScoped<IUserService, UserService>();
            _sc_services.AddScoped<IVerbServices, VerbServices>();
            _sc_services.AddScoped<ISentenceStructureServices, SentenceStructureServices>();
            _sc_services.AddScoped<ISentenceFormsServices, SentenceFormsServices>();
            _sc_services.AddScoped<ISubCategoryServices, SubCategoryServices>();
            _sc_services.AddScoped<ICategoryServices, CategoryServices>();
            _sc_services.AddScoped<IDescriptionServices, DescriptionServices>();
            _sc_services.AddScoped<IAddsServices,AddsServices>();
            _sc_services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 10 * 1024 * 1024;
            });
            return _sc_services; 
        }
    }
}
