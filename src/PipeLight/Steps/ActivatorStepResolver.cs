using PipeLight.Abstractions.Steps;
using PipeLight.Exceptions;

namespace PipeLight.Steps;

internal sealed class ActivatorStepResolver : IStepResolver
{
    public IPipelineAction<T> ResolveAction<T>(Type stepType)
        => (IPipelineAction<T>)(Activator.CreateInstance(stepType) ?? throw new StepResolveException());

    public IPipelineTransform<TIn, TOut> ResolveTransform<TIn, TOut>(Type transformType)
        => (IPipelineTransform<TIn, TOut>)(Activator.CreateInstance(transformType) ?? throw new StepResolveException());
    
    public IPipelineSeal<T> ResolveSeal<T>(Type sealedStepType)
        => (IPipelineSeal<T>)(Activator.CreateInstance(sealedStepType) ?? throw new StepResolveException());
}