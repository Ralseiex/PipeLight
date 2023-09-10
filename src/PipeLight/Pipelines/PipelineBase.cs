using PipeLight.Abstractions.Builders;
using PipeLight.Abstractions.Pipelines;
using PipeLight.Abstractions.Steps;
using PipeLight.Exceptions;
using PipeLight.Factories;

namespace PipeLight.Pipelines;

public abstract class PipelineBase<TIn> : IPipeline<TIn>
{
    private IPipeline<TIn>? _pipeline;
    public abstract void ConfigurePipeline(IPipelineBuilder<TIn> builder);

    public void ConfigurePipeline(IStepResolver stepResolver)
    {
        var builder = new PipelineBuilderFactory(stepResolver).CreateBuilder<TIn>();
        ConfigurePipeline(builder);
        _pipeline = builder.Build();
    }

    public async Task Push(TIn payload, CancellationToken cancellationToken = default)
    {
        if (_pipeline is null)
            throw new PipelineNotConfiguredException();
        await _pipeline.Push(payload, cancellationToken);
    }
}