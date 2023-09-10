using PipeLight.Abstractions.Context;
using PipeLight.Abstractions.Pipes;
using PipeLight.Abstractions.Steps;
using PipeLight.Exceptions;

namespace PipeLight.Pipes;

internal sealed class SealedPipe<T> : ISealedPipe<T>
{
    private readonly IPipelineSeal<T> _step;

    public SealedPipe(IPipelineSeal<T> step)
    {
        Id = Guid.NewGuid();
        _step = step;
    }

    public Guid Id { get; }

    public Task Push(object payload, IPipelineContext context)
    {
        if (payload is not T typedPayload) throw new InvalidPayloadTypeException();
        return Push(typedPayload, context);
    }

    public async Task Push(T payload, IPipelineContext context)
    {
        try
        {
            context.CancellationToken.ThrowIfCancellationRequested();
            await _step.Execute(payload).ConfigureAwait(false);
            context.PipelineCompletionSource.SetResult(null);
        }
        catch (Exception ex)
        {
            context.PipelineCompletionSource.SetException(ex);
        }
    }
}