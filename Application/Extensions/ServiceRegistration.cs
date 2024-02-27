using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ServiceRegistration
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
}