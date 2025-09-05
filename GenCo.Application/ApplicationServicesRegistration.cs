using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using GenCo.Application.Behaviors;

namespace GenCo.Application;

public static class ApplicationServicesRegistration
{
    public static void ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        // MediatR
        services.AddMediatR(Assembly.GetExecutingAssembly());
        // Exception handling pipeline
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlingBehavior<,>));
    }
}
