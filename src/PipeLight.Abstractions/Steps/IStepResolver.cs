namespace PipeLight.Abstractions.Steps;

public interface IStepResolver
{
    IPipelineAction<T> ResolveAction<T>(Type stepType);
    IPipelineTransform<TIn, TOut> ResolveTransform<TIn, TOut>(Type transformType);
    IPipelineSeal<T> ResolveSeal<T>(Type sealedStepType);
}