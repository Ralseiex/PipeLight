using PipeLight.Builders;
using PipeLight.Steps;

namespace PipeLight.Factories;

public class ActivatorPipelineBuilderFactory : IPipelineBuilderFactory
{
    public PipelineBuilder CreateBuilder() 
        => new(new ActivatorStepResolver());

    // public PipelineBuilder<T> CreateBuilder<T>() 
    //     => new(new ActivatorStepResolver());
}