using Microsoft.Extensions.DependencyInjection;
using PipeLight.Builders;
using PipeLight.Factories;

namespace PipeLight.DependencyInjection;

public class MsDiPipelineBuilderFactory : IPipelineBuilderFactory
{
    private readonly IServiceScope _serviceScope;

    public MsDiPipelineBuilderFactory(IServiceScope serviceScope)
    {
        _serviceScope = serviceScope;
    }

    public PipelineBuilder CreateBuilder()
        => new(new MsDiStepResolver(_serviceScope.ServiceProvider));
}
