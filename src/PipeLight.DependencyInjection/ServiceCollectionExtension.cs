using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PipeLight.Abstractions.Steps;

namespace PipeLight.DependencyInjection;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddPipelines(this IServiceCollection services, params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            var typesToRegister = assembly.GetTypes().Where(x => typeof(IPipelineStep).IsAssignableFrom(x));
            foreach (var type in typesToRegister)
            {
                services.TryAddScoped(type);
            }
        }

        return services;
    }
}