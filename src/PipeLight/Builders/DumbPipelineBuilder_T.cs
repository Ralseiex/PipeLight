using PipeLight.Abstractions.Builders;
using PipeLight.Abstractions.Pipelines;
using PipeLight.Abstractions.Steps;
using PipeLight.Pipelines;

namespace PipeLight.Builders;

public sealed class DumbPipelineBuilder<T> : IPipelineBuilder<T>
{
    private readonly IStepResolver _stepResolver;

    public DumbPipelineBuilder(IStepResolver stepResolver)
    {
        _stepResolver = stepResolver;
    }

    public int PipelineLength => 0;

    public IPipelineBuilder<T> AddAction(IPipelineAction<T> action) 
        => new PipelineBuilder<T>(_stepResolver, action);

    public IPipelineBuilder<T> AddAction(Type stepType)
        => AddAction(_stepResolver.ResolveAction<T>(stepType));

    public IPipelineBuilder<T> AddAction<TStep>()
        => AddAction(typeof(TStep));

    public IPipelineBuilder<T, TNewOut> AddTransform<TNewOut>(IPipelineTransform<T, TNewOut> transform) 
        => new PipelineBuilder<T, TNewOut>(_stepResolver, transform);

    public IPipelineBuilder<T, TNewOut> AddTransform<TNewOut>(Type transformType)
        => AddTransform(_stepResolver.ResolveTransform<T, TNewOut>(transformType));

    public IPipelineBuilder<T, TNewOut> AddTransform<TNewOut, TStep>()
        => AddTransform<TNewOut>(typeof(TStep));


    public ISealedPipelineBuilder<T> Seal(IPipelineSeal<T> lastStep) 
        => new SealedPipelineBuilder<T>(_stepResolver, lastStep);

    public ISealedPipelineBuilder<T> Seal(Type sealedStepType)
        => Seal(_stepResolver.ResolveSeal<T>(sealedStepType));

    public ISealedPipelineBuilder<T> Seal<TStep>()
        => Seal(typeof(TStep));

    public IPipeline<T> Build() => new DumbPipeline<T>();
}