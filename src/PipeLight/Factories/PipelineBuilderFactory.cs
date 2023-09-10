using PipeLight.Abstractions.Builders;
using PipeLight.Abstractions.Factories;
using PipeLight.Abstractions.Steps;
using PipeLight.Builders;

namespace PipeLight.Factories;

public class PipelineBuilderFactory : IPipelineBuilderFactory
{
    private readonly IStepResolver _stepResolver;

    public PipelineBuilderFactory(IStepResolver stepResolver)
    {
        _stepResolver = stepResolver;
    }

    public IPipelineBuilder CreateBuilder()
        => new PipelineBuilder(_stepResolver);

    public IPipelineBuilder<T> CreateBuilder<T>()
        => new DumbPipelineBuilder<T>(_stepResolver);
}