namespace PipeLight.Steps.Interfaces;

public interface IPipelineStepAsyncHandler<in TIn>
{
    Task InvokeAsync(TIn payload);
}
public interface IPipelineStepAsyncHandler<in TIn, TOut>
{
    Task<TOut> InvokeAsync(TIn payload);
}