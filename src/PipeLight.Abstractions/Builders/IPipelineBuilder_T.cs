using PipeLight.Abstractions.Pipelines;
using PipeLight.Abstractions.Steps;

namespace PipeLight.Abstractions.Builders;

public interface IPipelineBuilder<T>
{
    IPipelineBuilder<T> AddStep(IPipelineStep<T> step);
    IPipelineBuilder<T> AddStep(Type stepType);
    IPipelineBuilder<T> AddStep<TStep>();
    IPipelineBuilder<T, TNewOut> AddTransform<TNewOut>(IPipelineTransform<T, TNewOut> transform);
    IPipelineBuilder<T, TNewOut> AddTransform<TNewOut>(Type transformType);
    IPipelineBuilder<T, TNewOut> AddTransform<TNewOut, TStep>();
    ISealedPipelineBuilder<T> Seal(IPipelineSealedStep<T> lastStep);
    ISealedPipelineBuilder<T> Seal(Type sealedStepType);
    ISealedPipelineBuilder<T> Seal<TStep>();
    IPipeline<T> Build();
}