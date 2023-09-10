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

    public IPipelineAction<T> ResolveAction<T>(Type stepType)
        => (IPipelineAction<T>)(_serviceProvider.GetService(stepType) ?? throw new StepResolveException());

    public IPipelineTransform<TIn, TOut> ResolveTransform<TIn, TOut>(Type transformType)
        => (IPipelineTransform<TIn, TOut>)(_serviceProvider.GetService(transformType) ?? throw new StepResolveException());
    
    public IPipelineSeal<T> ResolveSeal<T>(Type sealedStepType)
        => (IPipelineSeal<T>)(_serviceProvider.GetService(sealedStepType) ?? throw new StepResolveException());
}