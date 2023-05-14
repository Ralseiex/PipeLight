using PipeLight.Abstractions.Steps;

namespace PipeLight.Abstractions.Builders;

public interface IPipelineBuilder
{
    IPipelineBuilder<T> AddStep<T>(IPipelineStep<T> step);
    IPipelineBuilder<T> AddStep<T>(Type stepType);
    IPipelineBuilder<T> AddStep<T, TStep>();
    IPipelineBuilder<TIn, TNewOut> AddTransform<TIn, TNewOut>(IPipelineTransform<TIn, TNewOut> transform);
    IPipelineBuilder<TIn, TNewOut> AddTransform<TIn, TNewOut>(Type transformType);
    IPipelineBuilder<TIn, TNewOut> AddTransform<TIn, TNewOut, TStep>();
    ISealedPipelineBuilder<T> Seal<T>(IPipelineSealedStep<T> singleStep);
    ISealedPipelineBuilder<T> Seal<T>(Type sealedStepType);
    ISealedPipelineBuilder<T> Seal<T, TStep>();
}