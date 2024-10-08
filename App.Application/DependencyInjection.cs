using App.Application.Features.EmployeeFeatures.CommandHandlers;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;


namespace App.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection srv)
        {

            // Register MediatR handlers from the assembly
            srv.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            // Register FluentValidation validators from the same assembly
            srv.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateEmployeeValidator>());

            
            return srv;
        }
    }
}
