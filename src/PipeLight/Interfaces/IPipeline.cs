using PipeLight.Steps.Interfaces;

namespace PipeLight.Interfaces;

public interface ISealedPipeline<TIn>
{
    Task PushAsync(TIn payload);
}
public interface IPipeline<T>
{
    Task<T> PushAsync(T payload);
    IPipeline<T> AddStep(IPipelineStepAsyncHandler<T, T> stepHandler);
    IPipeline<T, TNewOut> AddStep<TNewOut>(IPipelineStepAsyncHandler<T, TNewOut> stepHandler);
    ISealedPipeline<T> AddStep(IPipelineStepAsyncHandler<T> lastStepHandler);
}
public interface IPipeline<TIn, TOut>
{
    Task<TOut> PushAsync(TIn payload);
    IPipeline<TIn, TNewOut> AddStep<TNewOut>(IPipelineStepAsyncHandler<TOut, TNewOut> stepHandler);
    ISealedPipeline<TIn> AddStep(IPipelineStepAsyncHandler<TOut> lastStepHandler);
}