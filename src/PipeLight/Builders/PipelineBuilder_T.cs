using PipeLight.Abstractions.Builders;
using PipeLight.Abstractions.Pipelines;
using PipeLight.Abstractions.Steps;
using PipeLight.Pipelines;
using PipeLight.Pipes;

namespace PipeLight.Builders;

public class PipelineBuilder<T> : IPipelineBuilder<T>
{
    private readonly IStepResolver _stepResolver;
    private readonly List<IPipelineStep<T>> _steps = new();

    public PipelineBuilder(IStepResolver stepResolver)
    {
        _stepResolver = stepResolver;
    }

    public PipelineBuilder(IStepResolver stepResolver, IPipelineStep<T> firstStep) : this(stepResolver)
    {
        _steps.Add(firstStep);
    }

    public IPipelineBuilder<T> AddStep(IPipelineStep<T> step)
    {
        _steps.Add(step);
        return this;
    }
    public IPipelineBuilder<T> AddStep(Type stepType)
    {
        var step = _stepResolver.ResolveStep<T>(stepType);
        return AddStep(step);
    }
    public IPipelineBuilder<T> AddStep<TStep>()
        => AddStep(typeof(TStep));
    
    public IPipelineBuilder<T, TNewOut> AddTransform<TNewOut>(IPipelineTransform<T, TNewOut> transform)
    {
        var currentPipe = new ActionPipe<T>(_steps);
        var transformPipe = new TransformPipe<T, TNewOut>(transform);
        var builder = new PipelineBuilder<T, TNewOut>(_stepResolver, currentPipe, transformPipe);
        return builder;
    }
    public IPipelineBuilder<T, TNewOut> AddTransform<TNewOut>(Type transformType)
    {
        var transform = _stepResolver.ResolveTransform<T, TNewOut>(transformType);
        return AddTransform(transform);
    }
    public IPipelineBuilder<T, TNewOut> AddTransform<TNewOut, TStep>()
        => AddTransform<TNewOut>(typeof(TStep));

    public ISealedPipelineBuilder<T> Seal(IPipelineSealedStep<T> lastStep)
    {
        var currentPipe = new ActionPipe<T>(_steps);
        var sealedPipe = new SealedPipe<T>(lastStep);
        currentPipe.NextPipe = sealedPipe;
        return new SealedPipelineBuilder<T>(_stepResolver, currentPipe);
    }
    public ISealedPipelineBuilder<T> Seal(Type sealedStepType)
    {
        var step = _stepResolver.ResolveSealedStep<T>(sealedStepType);
        return Seal(step);
    }
    public ISealedPipelineBuilder<T> Seal<TStep>()
        => Seal(typeof(TStep));

    public IPipeline<T> Build()
    {
        return new Pipeline<T>(_steps);
    }
}