using PipeLight.Abstractions.Pipelines;
using PipeLight.Abstractions.Steps;

namespace PipeLight.Abstractions.Builders;

public interface IPipelineBuilder<T>
{
    int PipelineLength { get; }
    IPipelineBuilder<T> AddAction(IPipelineAction<T> action);
    IPipelineBuilder<T> AddAction(Type stepType);
    IPipelineBuilder<T> AddAction<TStep>();
    IPipelineBuilder<T, TNewOut> AddTransform<TNewOut>(IPipelineTransform<T, TNewOut> transform);
    IPipelineBuilder<T, TNewOut> AddTransform<TNewOut>(Type transformType);
    IPipelineBuilder<T, TNewOut> AddTransform<TNewOut, TStep>();
    ISealedPipelineBuilder<T> Seal(IPipelineSeal<T> lastStep);
    ISealedPipelineBuilder<T> Seal(Type sealedStepType);
    ISealedPipelineBuilder<T> Seal<TStep>();
    IPipeline<T> Build();
}
