using System.Linq;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection.Extensions;

using RemainingMediator;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddRemainingMediator(this IServiceCollection services,
                                                 ServiceLifetime serviceLifetime = ServiceLifetime.Transient,
                                                 params Assembly[] assemblies)
    {
        services.AddTransient<IMediator, Mediator>();
        
        var interfaces = new[]
        {
            typeof(IRequestHandler<>),
            typeof(IRequestHandler<,>),
            typeof(INotificationHandler<>)
        };

        foreach (var assembly in assemblies)
        {
            var handlers = assembly
                .GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface)
                .SelectMany(t => t.GetInterfaces(), (t, i) => new { Type = t, Interface = i })
                .Where(ti => ti.Interface.IsGenericType && interfaces.Contains(ti.Interface.GetGenericTypeDefinition()));

            foreach (var handler in handlers) 
                services.TryAdd(ServiceDescriptor.Describe(handler.Interface, handler.Type, serviceLifetime));
        }

        return services;
    }
}