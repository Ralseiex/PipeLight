using PipeLight.Abstractions.Pipelines;
using PipeLight.Abstractions.Steps;
using PipeLight.Pipelines;
using PipeLight.Pipes;

namespace PipeLight.Builders;

public class PipelineBuilder<T>
{
    private readonly List<IPipelineStep<T>> _steps = new();

    public PipelineBuilder()
    {
    }

    public PipelineBuilder(IPipelineStep<T> firstStep)
    {
        _steps.Add(firstStep);
    }

    public PipelineBuilder<T> AddStep(IPipelineStep<T> step)
    {
        _steps.Add(step);
        return this;
    }

    public PipelineBuilder<T, TNewOut> AddTransform<TNewOut>(IPipelineTransformStep<T, TNewOut> transformStep)
    {
        var currentPipe = new ActionPipe<T>(_steps);
        var transformPipe = new TransformPipe<T, TNewOut>(transformStep);
        var builder = new PipelineBuilder<T, TNewOut>(currentPipe, transformPipe);
        return builder;
    }

    public SealedPipelineBuilder<T> Seal(IPipelineSealedStep<T> lastStep)
    {
        var currentPipe = new ActionPipe<T>(_steps);
        var sealedPipe = new SealedPipe<T>(lastStep);
        currentPipe.NextPipe = sealedPipe;
        return new SealedPipelineBuilder<T>(currentPipe);
    }

    public IPipeline<T> Build()
    {
        return new Pipeline<T>(_steps);
    }
}