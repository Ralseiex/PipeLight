using PipeLight.Abstractions.Pipelines;
using PipeLight.Abstractions.Steps;

namespace PipeLight.Abstractions.Builders;

public interface IPipelineBuilder<in TIn, TOut>
{
    int PipelineLength { get; }
    IPipelineBuilder<TIn, TOut> AddStep(IPipelineStep<TOut> step);
    IPipelineBuilder<TIn, TOut> AddStep(Type stepType);
    IPipelineBuilder<TIn, TOut> AddStep<TStep>();
    IPipelineBuilder<TIn, TNewOut> AddTransform<TNewOut>(IPipelineTransform<TOut, TNewOut> transform);
    IPipelineBuilder<TIn, TNewOut> AddTransform<TNewOut>(Type transformType);
    IPipelineBuilder<TIn, TNewOut> AddTransform<TNewOut, TStep>();
    ISealedPipelineBuilder<TIn> Seal(IPipelineSealedStep<TOut> lastStep);
    ISealedPipelineBuilder<TIn> Seal(Type sealedStepType);
    ISealedPipelineBuilder<TIn> Seal<TStep>();
    IPipelineWithOutput<TIn, TOut> Build();
}
