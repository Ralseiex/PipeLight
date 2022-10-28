using PipeLight.Interfaces;
using PipeLight.Steps.Delegates;
using PipeLight.Steps.Interfaces;

namespace PipeLight.Extensions;

public static class InterfacePipelineExtensions
{
    public static ISealedPipeline<TIn> Seal<TIn, TOut>(this IPipeline<TIn, TOut> pipeline, IPipelineStepAsyncHandler<TOut> lastStepHandler)
        => pipeline.AddStep(lastStepHandler);
    public static ISealedPipeline<TIn> Seal<TIn, TOut>(this IPipeline<TIn, TOut> pipeline, Action<TOut> action)
        => pipeline.AddStep(action);
    public static IPipeline<TIn, TNewOut> AddStep<TIn, TOut, TNewOut>(this IPipeline<TIn, TOut> pipeline, Func<TOut, TNewOut> func)
    {
        var step = new FuncStepAsync<TOut, TNewOut>(func);
        return pipeline.AddStep(step);
    }
    public static ISealedPipeline<TIn> AddStep<TIn, TOut>(this IPipeline<TIn, TOut> pipeline, Action<TOut> action)
    {
        var lastStep = new ActionSealAsync<TOut>(action);
        return pipeline.AddStep(lastStep);
    }
}
