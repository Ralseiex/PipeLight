using PipeLight.Abstractions.Steps;
using PipeLight.Steps;

namespace PipeLight.Builders;

public class PipelineBuilder
{
    private readonly IStepResolver _stepResolver;

    public PipelineBuilder(IStepResolver stepResolver)
    {
        _stepResolver = stepResolver;
    }

    public PipelineBuilder<T> AddStep<T>(IPipelineStep<T> step) 
        => new(_stepResolver, step);
    public PipelineBuilder<T> AddStep<T>(Type stepType) 
        => new(_stepResolver, _stepResolver.ResolveStep<T>(stepType));
    public PipelineBuilder<T> AddStep<T, TStep>() 
        => AddStep<T>(typeof(TStep));

    public PipelineBuilder<TIn, TNewOut> AddTransform<TIn, TNewOut>(IPipelineTransform<TIn, TNewOut> transform) 
        => new(_stepResolver, transform);
    public PipelineBuilder<TIn, TNewOut> AddTransform<TIn, TNewOut>(Type transformType) 
        => new(_stepResolver, transformType);
    public PipelineBuilder<TIn, TNewOut> AddTransform<TIn, TNewOut, TStep>() 
        => AddTransform<TIn, TNewOut>(typeof(TStep));

    public SealedPipelineBuilder<T> Seal<T>(IPipelineSealedStep<T> singleStep) 
        => new(_stepResolver, singleStep);
    public SealedPipelineBuilder<T> Seal<T>(Type sealedStepType) 
        => new(_stepResolver, sealedStepType);
    public SealedPipelineBuilder<T> Seal<T, TStep>() 
        => Seal<T>(typeof(TStep));
}