using PipeLight.Interfaces;
using PipeLight.Interfaces.Steps;
using PipeLight.Steps.Delegates;
using PipeLight.Steps.Interfaces;

namespace PipeLight.Base;

public abstract class PipelineBase<TIn> : ISealedPipeline<TIn>
{
    protected PipelineBase() { }
    public PipelineBase(IPipelineStepEnter<TIn> firstStep)
    {
        FirstStep = firstStep;
    }


    public IPipelineStepEnter<TIn> FirstStep { get; init; }


    public async Task PushAsync(TIn data)
    {
        var pipelineCompletionSource = new TaskCompletionSource<object>();
        var task = pipelineCompletionSource.Task;
        var context = new PipelineContext(pipelineCompletionSource);

        FirstStep?.PushAsync(data, context).ConfigureAwait(false);

        await task;
    }
}
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

    public IPipeline<TIn, TNewOut> AddStep<TNewOut>(IPipelineStepAsyncHandler<TOut, TNewOut> stepHandler)
    {
        var newStep = new PipelineStep<TOut, TNewOut>(stepHandler);

        LastStep.NextStep = newStep;
        return new Pipeline<TIn, TNewOut>(FirstStep, newStep);
    }
    public ISealedPipeline<TIn> AddStep(IPipelineStepAsyncHandler<TOut> lastStepHandler)
    {
        var newStep = new PipelineStep<TOut>(lastStepHandler);

        LastStep.NextStep = newStep;
        return new SealedPipeline<TIn>(FirstStep);
    }
    public IPipeline<TIn, TNewOut> AddStep<T, TNewOut>() where T : IPipelineStepAsyncHandler<TOut, TNewOut>
    {
        // TODO: Добавить ресолвер
        var stepHandler = (IPipelineStepAsyncHandler<TOut, TNewOut>)Activator.CreateInstance(typeof(T));
        var newStep = new PipelineStep<TOut, TNewOut>(stepHandler);

        LastStep.NextStep = newStep;
        return new Pipeline<TIn, TNewOut>(FirstStep, newStep);
    }
}
