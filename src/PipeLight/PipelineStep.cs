using PipeLight.Exceptions;
using PipeLight.Interfaces;
using PipeLight.Interfaces.Steps;
using PipeLight.Steps.Interfaces;

namespace PipeLight;

internal class PipelineStep<TIn> : IPipelineStep<TIn>
{
    private readonly IPipelineStepAsyncHandler<TIn> _asyncStepHandler;

    public PipelineStep(IPipelineStepAsyncHandler<TIn> step)
    {
        _asyncStepHandler = step;
    }


    public async Task PushAsync(TIn payload, IPipelineContext context)
    {
        try
        {
            await _asyncStepHandler.InvokeAsync(payload).ConfigureAwait(false);
            context.PipelineCompletionSource?.SetResult(null);
        }
        catch (Exception ex)
        {
            context.PipelineCompletionSource?.SetException(ex);
        }
    }
}
internal class PipelineStep<TIn, TOut> : IPipelineStep<TIn, TOut>
{
    private readonly IPipelineStepAsyncHandler<TIn, TOut> _asyncStepHandler;
    public PipelineStep(IPipelineStepAsyncHandler<TIn, TOut> step)
    {
        _asyncStepHandler = step;
    }

    public IPipelineStepEnter<TOut>? NextStep { get; set; }


    public async Task PushAsync(TIn? payload, IPipelineContext context)
    {
        try
        {
            var result = await _asyncStepHandler.InvokeAsync(payload).ConfigureAwait(false);

            if (NextStep is not null)
                _ = NextStep.PushAsync(result, context).ConfigureAwait(false);
            else
                context.PipelineCompletionSource?.SetResult(result);
        }
        catch (Exception ex)
        {
            context.PipelineCompletionSource?.SetException(ex);
        }
    }
}