using PipeLight.Abstractions.Pipes;
using PipeLight.Context;

namespace PipeLight.Pipelines;

public abstract class PipelineCore<TIn>
{
    protected readonly IPipeEnter<TIn> FirstPipe;

    protected PipelineCore(IPipeEnter<TIn> firstPipe)
    {
        FirstPipe = firstPipe;
    }

    protected static Task<object> Push(TIn payload, IPipeEnter enterPipe, CancellationToken cancellationToken)
    {
        if (payload is null)
            throw new NullReferenceException(nameof(payload));

        var pipelineCompletionSource = new TaskCompletionSource<object?>();
        var context = new PipelineContext(Guid.NewGuid(), pipelineCompletionSource, cancellationToken);

        enterPipe.Push(payload, context);

        return pipelineCompletionSource.Task!;
    }

    protected static Task<object> Push(object payload, IPipeEnter enterPipe, CancellationToken cancellationToken)
    {
        if (payload is null)
            throw new NullReferenceException(nameof(payload));

        var pipelineCompletionSource = new TaskCompletionSource<object?>();
        var context = new PipelineContext(Guid.NewGuid(), pipelineCompletionSource, cancellationToken);

        enterPipe.Push(payload, context);

        return pipelineCompletionSource.Task!;
    }
}