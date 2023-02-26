using PipeLight.Abstractions.Pipelines;
using PipeLight.Builders;
using PipeLight.Steps;

namespace PipeLight.Pipelines;

public abstract class PipelineBase<TIn, TOut> : IPipeline<TIn, TOut>
{
    private readonly IPipeline<TIn, TOut> _innerPipeline;

    protected PipelineBase()
    {
        var builder = new PipelineBuilder<TIn>(new ActivatorStepResolver());
        _innerPipeline = Configure(builder).Build();
    }

    protected abstract PipelineBuilder<TIn, TOut> Configure(PipelineBuilder<TIn> builder);

    public async Task<TOut> Push(TIn payload)
    {
        return await _innerPipeline.Push(payload);
    }
}

public abstract class PipelineBase<T> : IPipeline<T>
{
    private readonly IPipeline<T> _innerPipeline;

    protected PipelineBase()
    {
        var builder = new PipelineBuilder<T>(new ActivatorStepResolver());
        _innerPipeline = Configure(builder).Build();
    }

    protected abstract PipelineBuilder<T> Configure(PipelineBuilder<T> builder);

    public Task<T> Push(T payload)
    {
        return _innerPipeline.Push(payload);
    }
}