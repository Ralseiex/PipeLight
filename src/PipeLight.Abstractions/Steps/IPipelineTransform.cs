namespace PipeLight.Abstractions.Steps;

public interface IPipelineTransform<in TIn, TOut> : IPipelineStep
{
    Task<TOut> Transform(TIn payload);
}