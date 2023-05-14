using PipeLight.Abstractions.Context;
using PipeLight.Abstractions.Pipes;
using PipeLight.Abstractions.Steps;

namespace PipeLight.Pipes;

internal class ActionPipe<T> : IActionPipe<T>
{
    private readonly IEnumerable<IPipelineStep<T>> _steps;

    public ActionPipe(IEnumerable<IPipelineStep<T>> steps)
    {
        _steps = steps;
    }

    public IPipeEnter<T>? NextPipe { get; set; }

    public async Task Push(T payload, IPipelineContext context)
    {
        try
        {
            context.CancellationToken.ThrowIfCancellationRequested();
            foreach (var step in _steps)
                payload = await step.Execute(payload).ConfigureAwait(false);

            if (NextPipe is not null)
                await NextPipe.Push(payload, context).ConfigureAwait(false);
            else
                context.PipelineCompletionSource.SetResult(payload);
        }
        catch (Exception ex)
        {
            context.PipelineCompletionSource.SetException(ex);
        }
    }
}