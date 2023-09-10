using Microsoft.Extensions.DependencyInjection;
using PipeLight.Factories;

namespace PipeLight.DependencyInjection;

public class MsDiPipelineBuilderFactory : PipelineBuilderFactory
{
    public MsDiPipelineBuilderFactory(IServiceScope serviceScope) 
        : base(new MsDiStepResolver(serviceScope.ServiceProvider))
    {
    }
}
