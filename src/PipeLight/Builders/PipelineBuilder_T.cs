using PipeLight.Abstractions.Builders;
using PipeLight.Abstractions.Pipelines;
using PipeLight.Abstractions.Pipes;
using PipeLight.Abstractions.Steps;
using PipeLight.Extensions;
using PipeLight.Pipelines;
using PipeLight.Pipes;

namespace PipeLight.Builders;

public class PipelineBuilder<T> : IPipelineBuilder<T>
{
    private readonly IStepResolver _stepResolver;
    private readonly IPipeEnter<T> _firstPipe;
    private IPipeExit<T> _lastPipe;
    private readonly List<IPipeEnter> _pipes;

    public PipelineBuilder(IStepResolver stepResolver, IPipelineStep<T> firstStep)
    {
        _stepResolver = stepResolver;
        var firstPipe = new ActionPipe<T>(firstStep);
        _firstPipe = firstPipe;
        _lastPipe = firstPipe;
        _pipes = new List<IPipeEnter>
        {
            _firstPipe
        };
    }

    public int PipelineLength => _pipes.Count;

    public IPipelineBuilder<T> AddStep(IPipelineStep<T> step)
    {
        var pipe = new ActionPipe<T>(step);
        SetNextPipe(pipe);
        _pipes.Add(pipe);
        return this;
    }

    public IPipelineBuilder<T> AddStep(Type stepType)
        => AddStep(_stepResolver.ResolveStep<T>(stepType));

    public IPipelineBuilder<T> AddStep<TStep>()
        => AddStep(typeof(TStep));

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

    public ISealedPipelineBuilder<T> Seal(IPipelineSealedStep<T> lastStep)
    {
        var pipe = new SealedPipe<T>(lastStep);
        _lastPipe.NextPipe = pipe;
        _pipes.Add(pipe);
        return new SealedPipelineBuilder<T>(_stepResolver, _firstPipe, _pipes);
    }

    public ISealedPipelineBuilder<T> Seal(Type sealedStepType)
        => Seal(_stepResolver.ResolveSealedStep<T>(sealedStepType));

    public ISealedPipelineBuilder<T> Seal<TStep>()
        => Seal(typeof(TStep));

    public IPipeline<T, T> Build()
        => new Pipeline<T, T>(_firstPipe, _pipes.ToPipesDictionary());

    private void SetNextPipe(IActionPipe<T> nextPipe)
    {
        _lastPipe.NextPipe = nextPipe;
        _lastPipe = nextPipe;
    }
}
