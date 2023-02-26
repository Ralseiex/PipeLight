using PipeLight.Abstractions.Steps;

namespace PipeLight.Steps;

public interface IStepResolver
{
    IPipelineStep<T> ResolveStep<T>(Type stepType);
    IPipelineTransform<TIn, TOut> ResolveTransform<TIn, TOut>(Type transformType);
    IPipelineSealedStep<T> ResolveSealedStep<T>(Type sealedStepType);
}