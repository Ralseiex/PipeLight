using PipeLight.Interfaces;
using PipeLight.Middlewares;
using PipeLight.Middlewares.Interfaces;

namespace PipeLight.Base;

public abstract class PipelineBase<TIn, TOut> : IPipeline<TIn, TOut>
{
    protected PipelineBase() { }
    public PipelineBase(IPipelineStepEnter<TIn> firstStep, IPipelineStepExit<TOut> lastStep)
    {
        FirstStep = firstStep;
        LastStep = lastStep;
    }


    public IPipelineStepEnter<TIn> FirstStep { get; init; }
    public IPipelineStepExit<TOut> LastStep { get; init; }


    public async Task<TOut> PushAsync(TIn data)
    {
        var pipelineCompletionSource = new TaskCompletionSource<object>();
        var task = pipelineCompletionSource.Task;
        var context = new PipelineContext(pipelineCompletionSource);

        FirstStep?.PushAsync(data, context).ConfigureAwait(false);

        return (TOut)(await task);
    }

    public IPipeline<TIn, TNewOut> AddMiddleware<TNewOut>(IPipelineMiddleware<TOut, TNewOut> middleware)
    {
        var newStep = new PipelineStep<TOut, TNewOut>(middleware);

        LastStep.NextStep = newStep;
        return new Pipeline<TIn, TNewOut>(FirstStep, newStep);
    }

    public IPipeline<TIn, TNewOut> AddFunc<TNewOut>(Func<TOut, TNewOut> func)
    {
        var middleware = new FuncMiddlware<TOut, TNewOut>(func);
        return AddMiddleware(middleware);
    }
}
