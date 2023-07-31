using PipeLight.Abstractions.Builders;
using PipeLight.Abstractions.Pipelines;
using PipeLight.Abstractions.Pipes;
using PipeLight.Abstractions.Steps;
using PipeLight.Extensions;
using PipeLight.Pipelines;
using PipeLight.Pipes;

namespace PipeLight.Builders;

public class SealedPipelineBuilder<T> : ISealedPipelineBuilder<T>
{
    private readonly IStepResolver _stepResolver;
    private readonly IPipeEnter<T> _firstPipe;
    private readonly List<IPipeEnter> _pipes;

    internal SealedPipelineBuilder(IStepResolver stepResolver, IPipeEnter<T> enter, List<IPipeEnter> pipes)
    {
        _stepResolver = stepResolver;
        _firstPipe = enter;
        _pipes = pipes;
    }

    public SealedPipelineBuilder(IStepResolver stepResolver, IPipelineSealedStep<T> singleStep)
    {
        _stepResolver = stepResolver;
        var sealedPipe = new SealedPipe<T>(singleStep);
        _firstPipe = sealedPipe;
        _pipes = new List<IPipeEnter>
        {
            _firstPipe
        };
    }

    public SealedPipelineBuilder(IStepResolver stepResolver, Type sealedStepType)
    {
        _stepResolver = stepResolver;
        var sealedStep = _stepResolver.ResolveSealedStep<T>(sealedStepType);
        var sealedPipe = new SealedPipe<T>(sealedStep);
        _firstPipe = sealedPipe;
        _pipes = new List<IPipeEnter>
        {
            _firstPipe
        };
    }

    public int PipelineLength => _pipes.Count;

    public ISealedPipeline<T> Build()
        => new SealedPipeline<T>(_firstPipe, _pipes.ToPipesDictionary());
}
