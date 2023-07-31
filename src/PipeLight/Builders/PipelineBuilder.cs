using PipeLight.Abstractions.Builders;
using PipeLight.Abstractions.Steps;

namespace PipeLight.Builders;

public class PipelineBuilder : IPipelineBuilder
{
    private readonly IStepResolver _stepResolver;

    public PipelineBuilder(IStepResolver stepResolver) 
        => _stepResolver = stepResolver;

    public IPipelineBuilder<T> AddStep<T>(IPipelineStep<T> step) 
        => new PipelineBuilder<T>(_stepResolver, step);
    public IPipelineBuilder<T> AddStep<T>(Type stepType) 
        => new PipelineBuilder<T>(_stepResolver, _stepResolver.ResolveStep<T>(stepType));
    public IPipelineBuilder<T> AddStep<T, TStep>() 
        => AddStep<T>(typeof(TStep));

    public IPipelineBuilder<TIn, TNewOut> AddTransform<TIn, TNewOut>(IPipelineTransform<TIn, TNewOut> transform) 
        => new PipelineBuilder<TIn, TNewOut>(_stepResolver, transform);
    public IPipelineBuilder<TIn, TNewOut> AddTransform<TIn, TNewOut>(Type transformType) 
        => new PipelineBuilder<TIn, TNewOut>(_stepResolver, transformType);
    public IPipelineBuilder<TIn, TNewOut> AddTransform<TIn, TNewOut, TStep>() 
        => AddTransform<TIn, TNewOut>(typeof(TStep));

    public ISealedPipelineBuilder<T> Seal<T>(IPipelineSealedStep<T> singleStep) 
        => new SealedPipelineBuilder<T>(_stepResolver, singleStep);
    public ISealedPipelineBuilder<T> Seal<T>(Type sealedStepType) 
        => new SealedPipelineBuilder<T>(_stepResolver, sealedStepType);
    public ISealedPipelineBuilder<T> Seal<T, TStep>() 
        => Seal<T>(typeof(TStep));
}
