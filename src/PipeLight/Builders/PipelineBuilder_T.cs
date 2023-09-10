using PipeLight.Abstractions.Builders;
using PipeLight.Abstractions.Pipelines;
using PipeLight.Abstractions.Pipes;
using PipeLight.Abstractions.Steps;
using PipeLight.Pipelines;
using PipeLight.Pipes;

namespace PipeLight.Builders;

public sealed class PipelineBuilder<T> : IPipelineBuilder<T>
{
    private readonly IStepResolver _stepResolver;
    private readonly IPipeEnter<T> _firstPipe;
    private IPipeExit<T> _lastPipe;
    private readonly List<IPipeEnter> _pipes;

    public PipelineBuilder(IStepResolver stepResolver, IPipelineAction<T> firstAction)
    {
        _stepResolver = stepResolver;
        var firstPipe = new ActionPipe<T>(firstAction);
        _firstPipe = firstPipe;
        _lastPipe = firstPipe;
        _pipes = new List<IPipeEnter>
        {
            _firstPipe
        };
    }

    public int PipelineLength => _pipes.Count;

    public IPipelineBuilder<T> AddAction(IPipelineAction<T> action)
    {
        var pipe = new ActionPipe<T>(action);
        SetNextPipe(pipe);
        _pipes.Add(pipe);
        return this;
    }

    public IPipelineBuilder<T> AddAction(Type stepType)
        => AddAction(_stepResolver.ResolveAction<T>(stepType));

    public IPipelineBuilder<T> AddAction<TStep>()
        => AddAction(typeof(TStep));

    public IPipelineBuilder<T, TNewOut> AddTransform<TNewOut>(IPipelineTransform<T, TNewOut> transform)
    {
        var transformPipe = new TransformPipe<T, TNewOut>(transform);
        _lastPipe.NextPipe = transformPipe;
        _pipes.Add(transformPipe);
        return new PipelineBuilder<T, TNewOut>(_stepResolver, _firstPipe, transformPipe, _pipes);
    }

    public IPipelineBuilder<T, TNewOut> AddTransform<TNewOut>(Type transformType)
        => AddTransform(_stepResolver.ResolveTransform<T, TNewOut>(transformType));

    public IPipelineBuilder<T, TNewOut> AddTransform<TNewOut, TStep>()
        => AddTransform<TNewOut>(typeof(TStep));

    public ISealedPipelineBuilder<T> Seal(IPipelineSeal<T> lastStep)
    {
        var pipe = new SealedPipe<T>(lastStep);
        _lastPipe.NextPipe = pipe;
        _pipes.Add(pipe);
        return new SealedPipelineBuilder<T>(_stepResolver, _firstPipe, _pipes);
    }

    public ISealedPipelineBuilder<T> Seal(Type sealedStepType)
        => Seal(_stepResolver.ResolveSeal<T>(sealedStepType));

    public ISealedPipelineBuilder<T> Seal<TStep>()
        => Seal(typeof(TStep));

    public IPipeline<T> Build()
        => new Pipeline<T>(_firstPipe);

    private void SetNextPipe(IActionPipe<T> nextPipe)
    {
        _lastPipe.NextPipe = nextPipe;
        _lastPipe = nextPipe;
    }
}