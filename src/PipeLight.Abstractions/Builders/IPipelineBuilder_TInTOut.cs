using PipeLight.Abstractions.Pipelines;
using PipeLight.Abstractions.Steps;

namespace PipeLight.Abstractions.Builders;

public interface IPipelineBuilder<in TIn, TOut>
{
    int PipelineLength { get; }
    IPipelineBuilder<TIn, TOut> AddAction(IPipelineAction<TOut> action);
    IPipelineBuilder<TIn, TOut> AddAction(Type stepType);
    IPipelineBuilder<TIn, TOut> AddAction<TStep>();
    IPipelineBuilder<TIn, TNewOut> AddTransform<TNewOut>(IPipelineTransform<TOut, TNewOut> transform);
    IPipelineBuilder<TIn, TNewOut> AddTransform<TNewOut>(Type transformType);
    IPipelineBuilder<TIn, TNewOut> AddTransform<TNewOut, TStep>();
    ISealedPipelineBuilder<TIn> Seal(IPipelineSeal<TOut> lastStep);
    ISealedPipelineBuilder<TIn> Seal(Type sealedStepType);
    ISealedPipelineBuilder<TIn> Seal<TStep>();
    IPipelineWithOutput<TIn, TOut> Build();
}
