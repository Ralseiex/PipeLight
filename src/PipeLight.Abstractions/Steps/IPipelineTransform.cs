namespace PipeLight.Abstractions.Steps;

public interface IPipelineTransform<in TIn, TOut>
{
    Task<TOut> Transform(TIn payload);
}