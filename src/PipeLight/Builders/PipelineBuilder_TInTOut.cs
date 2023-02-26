using PipeLight.Abstractions.Pipelines;
using PipeLight.Abstractions.Pipes;
using PipeLight.Abstractions.Steps;
using PipeLight.Pipelines;
using PipeLight.Pipes;
using PipeLight.Steps;

namespace PipeLight.Builders;

public class PipelineBuilder<TIn, TOut>
{
    private readonly List<IPipelineStep<TOut>> _steps = new();
    private readonly IStepResolver _stepResolver;
    private readonly IPipeEnter<TIn> _firstPipe;
    private readonly IPipeExit<TOut> _lastPipe;

    private PipelineBuilder(IStepResolver stepResolver, IPipeEnter<TIn> firstPipe, IPipeExit<TOut> lastPipe)
    {
        _stepResolver = stepResolver;
        _firstPipe = firstPipe;
        _lastPipe = lastPipe;
    }

    internal PipelineBuilder(IStepResolver stepResolver, IActionPipe<TIn> firstPipe, ITransformPipe<TIn, TOut> transform) 
    {
        _stepResolver = stepResolver;
        firstPipe.NextPipe = transform;
        _firstPipe = firstPipe;
        _lastPipe = transform;
    }

    public PipelineBuilder(IStepResolver stepResolver, IPipelineTransform<TIn, TOut> firstTransform)
    {
        _stepResolver = stepResolver;
        var transformPipe = new TransformPipe<TIn, TOut>(firstTransform);
        _firstPipe = transformPipe;
        _lastPipe = transformPipe;
    }
    
    public PipelineBuilder(IStepResolver stepResolver, Type firstTransformType)
    {
        _stepResolver = stepResolver;
        var firstTransform = _stepResolver.ResolveTransform<TIn, TOut>(firstTransformType);
        var transformPipe = new TransformPipe<TIn, TOut>(firstTransform);
        _firstPipe = transformPipe;
        _lastPipe = transformPipe;
    } 

    public PipelineBuilder<TIn, TOut> AddStep(IPipelineStep<TOut> step)
    {
        _steps.Add(step);
        return this;
    }
    public PipelineBuilder<TIn, TOut> AddStep(Type stepType)
    {
        var step = _stepResolver.ResolveStep<TOut>(stepType);
        return AddStep(step);
    }
    public PipelineBuilder<TIn, TOut> AddStep<TStep>()
        => AddStep(typeof(TStep));

    public PipelineBuilder<TIn, TNewOut> AddTransform<TNewOut>(IPipelineTransform<TOut, TNewOut> transform)
    {
        var currentPipe = new ActionPipe<TOut>(_steps);
        _lastPipe.NextPipe = currentPipe;
        var transformPipe = new TransformPipe<TOut, TNewOut>(transform);
        currentPipe.NextPipe = transformPipe;
        
        return new PipelineBuilder<TIn, TNewOut>(_stepResolver, _firstPipe, transformPipe);
    }
    public PipelineBuilder<TIn, TNewOut> AddTransform<TNewOut>(Type transformType)
    {
        var transform = _stepResolver.ResolveTransform<TOut, TNewOut>(transformType);
        return AddTransform(transform);
    }
    public PipelineBuilder<TIn, TNewOut> AddTransform<TNewOut, TStep>()
        => AddTransform<TNewOut>(typeof(TStep));

    public SealedPipelineBuilder<TIn> Seal(IPipelineSealedStep<TOut> lastStep)
    {
        var currentPipe = new ActionPipe<TOut>(_steps);
        _lastPipe.NextPipe = currentPipe;
        var sealedPipe = new SealedPipe<TOut>(lastStep);
        currentPipe.NextPipe = sealedPipe;

        return new SealedPipelineBuilder<TIn>(_stepResolver, _firstPipe);
    }
    public SealedPipelineBuilder<TIn> Seal(Type sealedStepType)
    {
        var sealedStep = _stepResolver.ResolveSealedStep<TOut>(sealedStepType);
        return Seal(sealedStep);
    }

    public SealedPipelineBuilder<TIn> Seal<TStep>()
        => Seal(typeof(TStep));
    
    public IPipeline<TIn, TOut> Build()
    {
        var currentPipe = new ActionPipe<TOut>(_steps);
        _lastPipe.NextPipe = currentPipe;
        return new Pipeline<TIn, TOut>(_firstPipe);
    }
}