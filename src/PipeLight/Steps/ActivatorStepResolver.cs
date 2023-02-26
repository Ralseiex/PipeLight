using PipeLight.Abstractions.Steps;
using PipeLight.Exceptions;

namespace PipeLight.Steps;

public class ActivatorStepResolver : IStepResolver
{
    public IPipelineStep<T> ResolveStep<T>(Type stepType)
        => (IPipelineStep<T>)(Activator.CreateInstance(stepType) ?? throw new StepResolveException());

    public IPipelineTransform<TIn, TOut> ResolveTransform<TIn, TOut>(Type transformType)
        => (IPipelineTransform<TIn, TOut>)(Activator.CreateInstance(transformType) ?? throw new StepResolveException());
    
    public IPipelineSealedStep<T> ResolveSealedStep<T>(Type sealedStepType)
        => (IPipelineSealedStep<T>)(Activator.CreateInstance(sealedStepType) ?? throw new StepResolveException());
}