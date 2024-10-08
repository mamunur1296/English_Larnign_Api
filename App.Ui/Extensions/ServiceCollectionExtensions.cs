using Microsoft.AspNetCore.Authentication.Cookies;
using System.Text;
using App.Ui.DTOs;
using App.Ui.Services.Interface;
using App.Ui.Services.Implemettions;
using App.Ui.Models;

namespace App.Ui.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Reading the BaseUrl value from configuration
            var baseUrl = configuration["BaseUrl:AuthenticationAPI"];
            services.Configure<ApiUrlSettings>(configuration.GetSection("ApiUrls"));

            // Assign it to Helper.BaseUrl if Helper is a static class
            UrlHelper.BaseUrl = baseUrl;

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            services.AddHttpClient();
            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IFileUploader, FileUploader>();
            services.AddScoped<IFileHelper, FileHelper>();
            services.AddScoped<IImageProcessor<Employee>, ImageProcessor>();
            services.AddScoped<IUtilityHelper, UtilityHelper>();

            
            services.AddScoped<IClientServices<Employee>, ClientServices<Employee>>();
            services.AddScoped<IClientServices<Login>, ClientServices<Login>>();
            services.AddScoped<IClientServices<Register>, ClientServices<Register>>();
            services.AddScoped<IClientServices<Menu>, ClientServices<Menu>>();
            services.AddScoped<IClientServices<SubMenu>, ClientServices<SubMenu>>();
            services.AddScoped<IClientServices<AssignActionsDTO>, ClientServices<AssignActionsDTO>>();
            services.AddScoped<IClientServices<ActionNameDTO>, ClientServices<ActionNameDTO>>();
            services.AddScoped<IClientServices<MenuDTO>, ClientServices<MenuDTO>>();
            services.AddScoped<IClientServices<PostMapingDTO>, ClientServices<PostMapingDTO>>();
            services.AddScoped<IClientServices<AssignedMenuDto>, ClientServices<AssignedMenuDto>>();
     
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Auth/Login";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(120);
                    options.ReturnUrlParameter = "ReturnUrl";
                });
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(120);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            return services;
        }
    }
}
