using PipeLight.Interfaces;
using PipeLight.Middlewares.Interfaces;

namespace PipeLight;

public class PipelineStep<TIn, TOut> : IPipelineStep<TIn, TOut>
{
    private readonly IPipelineMiddleware<TIn, TOut> _middleware;
    public PipelineStep(IPipelineMiddleware<TIn, TOut> middleware)
    {
        _middleware = middleware;
    }


    public IPipelineStepEnter<TOut>? NextStep { get; set; }


    public async Task PushAsync(TIn data, IPipelineContext context)
    {
        var result = await _middleware.InvokeAsync(data).ConfigureAwait(false);

        if (NextStep is not null)
            _ = NextStep.PushAsync(result, context).ConfigureAwait(false);
        else
            context.PipelineCompletionSource?.SetResult(result);
    }
}