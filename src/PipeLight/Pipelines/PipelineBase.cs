using PipeLight.Abstractions.Pipelines;
using PipeLight.Abstractions.Pipes;
using PipeLight.Context;

namespace PipeLight.Pipelines;

public abstract class PipelineCore<TIn>
{
    protected readonly IPipeEnter<TIn> FirstPipe;
    protected readonly ReadOnlyPipesDictionary Pipes;

    protected PipelineCore(IPipeEnter<TIn> firstPipe, PipesDictionary pipes)
    {
        FirstPipe = firstPipe;
        Pipes = new ReadOnlyPipesDictionary(pipes);
    }

    public IEnumerable<string> PipesHashes => Pipes.Keys;

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

public abstract class PipelineBase<TIn, TOut> : PipelineCore<TIn>, IPipeline<TIn, TOut>
{
    protected PipelineBase(IPipeEnter<TIn> firstPipe, PipesDictionary pipes) : base(firstPipe, pipes)
    {
    }

    public abstract Task<TOut> PushToPipe(object payload, string pipeId, CancellationToken cancellationToken = default);
    public abstract Task<TOut> Push(TIn payload, CancellationToken cancellationToken = default);
}

public abstract class SealedPipelineBase<TIn> : PipelineCore<TIn>, ISealedPipeline<TIn>
{
    protected SealedPipelineBase(IPipeEnter<TIn> firstPipe, PipesDictionary pipes) : base(firstPipe, pipes)
    {
    }

    public abstract Task PushToPipe(object payload, string pipeId, CancellationToken cancellationToken = default);
    public abstract Task Push(TIn payload, CancellationToken cancellationToken = default);
}