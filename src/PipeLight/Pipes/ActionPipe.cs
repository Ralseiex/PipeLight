using PipeLight.Abstractions.Context;
using PipeLight.Abstractions.Pipes;
using PipeLight.Abstractions.Steps;
using PipeLight.Exceptions;

namespace PipeLight.Pipes;

internal sealed class ActionPipe<T> : IActionPipe<T>
{
    private readonly IPipelineAction<T> _action;

    public ActionPipe(IPipelineAction<T> action)
    {
        Id = Guid.NewGuid();
        _action = action;
    }

    public Guid Id { get; }

    public Task Push(object payload, IPipelineContext context)
    {
        if (payload is not T typedPayload) throw new InvalidPayloadTypeException();
        return Push(typedPayload, context);
    }

    public IPipeEnter<T>? NextPipe { get; set; }

    public async Task Push(T payload, IPipelineContext context)
    {
        try
        {
            context.CancellationToken.ThrowIfCancellationRequested();
            var result = await _action.Execute(payload).ConfigureAwait(false);

            if (NextPipe is not null)
                await NextPipe.Push(result, context).ConfigureAwait(false);
            else
                context.PipelineCompletionSource.SetResult(result);
        }
        catch (Exception ex)
        {
            context.PipelineCompletionSource.SetException(ex);
        }
    }
}