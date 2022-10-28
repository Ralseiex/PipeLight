using PipeLight.Interfaces;
using PipeLight.Steps.Delegates;
using PipeLight.Steps.Interfaces;

namespace PipeLight.Extensions;

public static class PipelineExtensions
{
    public static ISealedPipeline<TIn> Seal<TIn>(this Pipeline pipeline, IPipelineStepAsyncHandler<TIn> lastStepHandler)
        => pipeline.AddStep(lastStepHandler);
    public static ISealedPipeline<TIn> Seal<TIn>(this Pipeline pipeline, Action<TIn> action)
        => pipeline.AddStep(action);
    public static IPipeline<TIn, TNewOut> AddStep<TIn, TNewOut>(this Pipeline pipeline, Func<TIn, TNewOut> func)
    {
        var step = new FuncStepAsync<TIn, TNewOut>(func);
        return pipeline.AddStep(step);
    }
    public static ISealedPipeline<TIn> AddStep<TIn>(this Pipeline pipeline, Action<TIn> action)
    {
        var lastStep = new ActionSealAsync<TIn>(action);
        return pipeline.AddStep(lastStep);
    }
}
