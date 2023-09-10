using PipeLight.Abstractions.Steps;

namespace PipeLight.Abstractions.Builders;

public interface IPipelineBuilder
{
    IPipelineBuilder<T> AddAction<T>(IPipelineAction<T> action);
    IPipelineBuilder<T> AddAction<T>(Type stepType);
    IPipelineBuilder<T> AddAction<T, TStep>();
    IPipelineBuilder<TIn, TNewOut> AddTransform<TIn, TNewOut>(IPipelineTransform<TIn, TNewOut> transform);
    IPipelineBuilder<TIn, TNewOut> AddTransform<TIn, TNewOut>(Type transformType);
    IPipelineBuilder<TIn, TNewOut> AddTransform<TIn, TNewOut, TStep>();
    ISealedPipelineBuilder<T> Seal<T>(IPipelineSeal<T> singleStep);
    ISealedPipelineBuilder<T> Seal<T>(Type sealedStepType);
    ISealedPipelineBuilder<T> Seal<T, TStep>();
}