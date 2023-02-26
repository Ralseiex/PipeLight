using PipeLight.Builders;
using PipeLight.Steps;

namespace PipeLight.Factories;

public interface IPipelineBuilderFactory
{
    PipelineBuilder CreateBuilder();
    PipelineBuilder<T> CreateBuilder<T>();
}

public class ActivatorPipelineBuilderFactory : IPipelineBuilderFactory
{
    public PipelineBuilder CreateBuilder() 
        => new(new ActivatorStepResolver());

    public PipelineBuilder<T> CreateBuilder<T>() 
        => new(new ActivatorStepResolver());
}