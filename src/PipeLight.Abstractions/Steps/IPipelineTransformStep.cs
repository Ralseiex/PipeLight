namespace PipeLight.Abstractions.Steps;

public interface IPipelineTransformStep<in TIn, TOut>
{
    Task<TOut> Transform(TIn payload);
}