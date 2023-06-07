using PipeLight.Abstractions.Builders;
using PipeLight.Abstractions.Pipelines;
using PipeLight.Abstractions.Pipes;
using PipeLight.Abstractions.Steps;
using PipeLight.Extensions;
using PipeLight.Pipelines;
using PipeLight.Pipes;

namespace PipeLight.Builders;

public class PipelineBuilder<TIn, TOut> : IPipelineBuilder<TIn, TOut>
{
    private readonly IStepResolver _stepResolver;
    private readonly IPipeEnter<TIn> _firstPipe;
    private IPipeExit<TOut> _lastPipe;
    private readonly List<IPipeEnter<TIn>> _pipes;

    private PipelineBuilder(IStepResolver stepResolver, IPipeEnter<TIn> firstPipe, IPipeExit<TOut> lastPipe)
    {
        _stepResolver = stepResolver;
        _firstPipe = firstPipe;
        _lastPipe = lastPipe;
        _pipes = new List<IPipeEnter<TIn>>();
    }

    internal PipelineBuilder(IStepResolver stepResolver, IPipeEnter<TIn> firstPipe, ITransformPipe<TIn, TOut> transform) 
    {
        _stepResolver = stepResolver;
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

    public IPipelineBuilder<TIn, TOut> AddStep(IPipelineStep<TOut> step)
    {
        var pipe = new ActionPipe<TOut>(step);
        SetNextPipe(pipe);
        _pipes.Add(pipe);
        return this;
    }
    public IPipelineBuilder<TIn, TOut> AddStep(Type stepType) 
        => AddStep(_stepResolver.ResolveStep<TOut>(stepType));

    public IPipelineBuilder<TIn, TOut> AddStep<TStep>()
        => AddStep(typeof(TStep));

    public IPipelineBuilder<TIn, TNewOut> AddTransform<TNewOut>(IPipelineTransform<TOut, TNewOut> transform)
    {
        var transformPipe = new TransformPipe<TOut, TNewOut>(transform);
        _lastPipe.NextPipe = transformPipe;
        _pipes.Add(transformPipe);
        return new PipelineBuilder<TIn, TNewOut>(_stepResolver, _firstPipe, transformPipe);
    }
    
    public IPipelineBuilder<TIn, TNewOut> AddTransform<TNewOut>(Type transformType) 
        => AddTransform(_stepResolver.ResolveTransform<TOut, TNewOut>(transformType));

    public IPipelineBuilder<TIn, TNewOut> AddTransform<TNewOut, TStep>()
        => AddTransform<TNewOut>(typeof(TStep));

    public ISealedPipelineBuilder<TIn> Seal(IPipelineSealedStep<TOut> lastStep)
    {
        _lastPipe.NextPipe = new SealedPipe<TOut>(lastStep);
        return new SealedPipelineBuilder<TIn>(_stepResolver, _firstPipe);
    }
    public ISealedPipelineBuilder<TIn> Seal(Type sealedStepType) 
        => Seal(_stepResolver.ResolveSealedStep<TOut>(sealedStepType));

    public ISealedPipelineBuilder<TIn> Seal<TStep>()
        => Seal(typeof(TStep));
    
    public IPipeline<TIn, TOut> Build() 
        => new Pipeline<TIn, TOut>(_firstPipe, _pipes.ToPipesDictionary());
    
    private void SetNextPipe(IActionPipe<TOut> nextPipe)
    {
        _lastPipe.NextPipe = nextPipe;
        _lastPipe = nextPipe;
    }
}