using PipeLight.Abstractions.Pipelines;
using PipeLight.Abstractions.Pipes;
using PipeLight.Abstractions.Steps;
using PipeLight.Pipelines;
using PipeLight.Pipes;

namespace PipeLight.Builders;

public class PipelineBuilder
{
    public PipelineBuilder<T> AddStep<T>(IPipelineStep<T> step)
    {
        return new PipelineBuilder<T>(step);
    }

    public PipelineBuilder<TIn, TNewOut> AddTransform<TIn, TNewOut>(IPipelineTransformStep<TIn, TNewOut> transformStep)
    {
        var transformPipe = new TransformPipe<TIn, TNewOut>(transformStep);
        return new PipelineBuilder<TIn, TNewOut>(transformPipe);
    }

    public SealedPipelineBuilder<T> Seal<T>(IPipelineSealedStep<T> singleStep)
    {
        var sealedPipe = new SealedPipe<T>(singleStep);
        return new SealedPipelineBuilder<T>(sealedPipe);
    }
}



