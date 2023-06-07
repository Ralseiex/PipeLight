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
    private readonly List<IPipeEnter<T>> _pipes;

    internal SealedPipelineBuilder(IStepResolver stepResolver, IPipeEnter<T> enter)
    {
        _stepResolver = stepResolver;
        _firstPipe = enter;
        _pipes = new List<IPipeEnter<T>>();
    }

    public SealedPipelineBuilder(IStepResolver stepResolver, IPipelineSealedStep<T> singleStep)
    {
        _stepResolver = stepResolver;
        var sealedPipe = new SealedPipe<T>(singleStep);
        _firstPipe = sealedPipe;
    }

    public SealedPipelineBuilder(IStepResolver stepResolver, Type sealedStepType)
    {
        _stepResolver = stepResolver;
        var sealedStep = _stepResolver.ResolveSealedStep<T>(sealedStepType);
        var sealedPipe = new SealedPipe<T>(sealedStep);
        _firstPipe = sealedPipe;
    }

    public ISealedPipeline<T> Build() 
        => new SealedPipeline<T>(_firstPipe, _pipes.ToPipesDictionary());
}