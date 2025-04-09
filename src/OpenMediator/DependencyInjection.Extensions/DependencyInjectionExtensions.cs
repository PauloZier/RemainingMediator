using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection.Extensions;

namespace OpenMediator;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddOpenMediator(this IServiceCollection services, 
                                                     IEnumerable<Assembly> assemblies,
                                                     ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
    {
        if (assemblies == null || !assemblies.Any()) throw new ArgumentException(nameof(assemblies));
        
        services.AddTransient<IMediatorHandler, MediatorHandler>();

        var interfaces = new[] { typeof(INotificationHandler<>), typeof(IRequestHandler<>), typeof(IRequestHandler<,>) };

        var types = assemblies
            .SelectMany(x => x.GetTypes())
            .Where(x => x.IsClass && !x.IsAbstract && x.GetInterfaces().Any(i => 
                i.IsGenericType && 
                interfaces.Contains(i.GetGenericTypeDefinition())));

        foreach (var @type in types) 
        {
            var @interface = interfaces.FirstOrDefault(x => @type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == x));
            services.TryAdd(ServiceDescriptor.Describe(@interface, @type, serviceLifetime));
        }        

        return services;
    }   
}