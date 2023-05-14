using PipeLight.Abstractions.Steps;
using PipeLight.Exceptions;

namespace PipeLight.DependencyInjection;

public class MsDiStepResolver : IStepResolver
{
    private readonly IServiceProvider _serviceProvider;

    public MsDiStepResolver(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IPipelineStep<T> ResolveStep<T>(Type stepType)
        => (IPipelineStep<T>)(_serviceProvider.GetService(stepType) ?? throw new StepResolveException());

    public IPipelineTransform<TIn, TOut> ResolveTransform<TIn, TOut>(Type transformType)
        => (IPipelineTransform<TIn, TOut>)(_serviceProvider.GetService(transformType) ?? throw new StepResolveException());
    
    public IPipelineSealedStep<T> ResolveSealedStep<T>(Type sealedStepType)
        => (IPipelineSealedStep<T>)(_serviceProvider.GetService(sealedStepType) ?? throw new StepResolveException());
}