using PipeLight.Abstractions.Pipelines;
using PipeLight.Abstractions.Pipes;
using PipeLight.Abstractions.Steps;
using PipeLight.Pipelines;
using PipeLight.Pipes;

namespace PipeLight.Builders;

public class PipelineBuilder<TIn, TOut>
{
    private readonly List<IPipelineStep<TOut>> _steps = new();
    private readonly IPipeEnter<TIn> _firstPipe;
    private readonly IPipeExit<TOut> _lastPipe;

    private PipelineBuilder(IPipeEnter<TIn> firstPipe, IPipeExit<TOut> lastPipe)
    {
        _firstPipe = firstPipe;
        _lastPipe = lastPipe;
    }

    public PipelineBuilder(IActionPipe<TIn> firstPipe, ITransformPipe<TIn, TOut> transform)
    {
        firstPipe.NextPipe = transform;
        _firstPipe = firstPipe;
        _lastPipe = transform;
    }

    public PipelineBuilder(ITransformPipe<TIn, TOut> firstTransform)
    {
        _firstPipe = firstTransform;
        _lastPipe = firstTransform;
    }

    public PipelineBuilder<TIn, TOut> AddStep(IPipelineStep<TOut> step)
    {
        _steps.Add(step);
        return this;
    }

    public PipelineBuilder<TIn, TNewOut> AddTransform<TNewOut>(IPipelineTransformStep<TOut, TNewOut> transformStep)
    {
        var currentPipe = new ActionPipe<TOut>(_steps);
        _lastPipe.NextPipe = currentPipe;
        var transformPipe = new TransformPipe<TOut, TNewOut>(transformStep);
        currentPipe.NextPipe = transformPipe;

        return new PipelineBuilder<TIn, TNewOut>(_firstPipe, transformPipe);
    }

    public SealedPipelineBuilder<TIn> Seal(IPipelineSealedStep<TOut> lastStep)
    {
        var currentPipe = new ActionPipe<TOut>(_steps);
        _lastPipe.NextPipe = currentPipe;
        var sealedPipe = new SealedPipe<TOut>(lastStep);
        currentPipe.NextPipe = sealedPipe;

        return new SealedPipelineBuilder<TIn>(_firstPipe);
    }
    
    public IPipeline<TIn, TOut> Build()
    {
        var currentPipe = new ActionPipe<TOut>(_steps);
        _lastPipe.NextPipe = currentPipe;
        return new Pipeline<TIn, TOut>(_firstPipe);
    }
}