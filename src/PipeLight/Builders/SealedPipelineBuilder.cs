using PipeLight.Abstractions.Pipelines;
using PipeLight.Abstractions.Pipes;
using PipeLight.Abstractions.Steps;
using PipeLight.Pipelines;
using PipeLight.Pipes;

namespace PipeLight.Builders;

public class SealedPipelineBuilder<T>
{
    private readonly IPipeEnter<T> _firstPipe;

    internal SealedPipelineBuilder(IPipeEnter<T> enter)
    {
        _firstPipe = enter;
    }

    public SealedPipelineBuilder(IPipelineSealedStep<T> singleStep)
    {
        var sealedPipe = new SealedPipe<T>(singleStep);
        _firstPipe = sealedPipe;
    }

    public ISealedPipeline<T> Build()
    {
        return new SealedPipeline<T>(_firstPipe);
    }
}