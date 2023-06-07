using PipeLight.Abstractions.Pipelines;
using PipeLight.Abstractions.Pipes;
using PipeLight.Context;
using PipeLight.Exceptions;

namespace PipeLight.Pipelines;

public class Pipeline<T> : Pipeline<T, T>, IPipeline<T>
{
    public Pipeline(IPipeEnter<T> firstPipe, PipesDictionary<T> pipes) : base(firstPipe, pipes)
    {
    }
}

public class Pipeline<TIn, TOut> : IPipeline<TIn, TOut>
{
    private readonly IPipeEnter<TIn> _firstPipe;
    private readonly ReadOnlyPipesDictionary<TIn> _pipes;

    public Pipeline(IPipeEnter<TIn> firstPipe, PipesDictionary<TIn> pipes)
    {
        _firstPipe = firstPipe;
        _pipes = new ReadOnlyPipesDictionary<TIn>(pipes);
    }
    
    public async Task<TOut> Push(TIn payload, CancellationToken cancellationToken = default)
    {
        return (TOut)await Push(payload, _firstPipe, cancellationToken).ConfigureAwait(false);
    }

    public async Task<TOut> PushToPipe(TIn payload, Guid pipeId, CancellationToken cancellationToken = default)
    {
        if (!_pipes.ContainsKey(pipeId))
            throw new PipeNotFoundException();
        return (TOut)await Push(payload, _pipes[pipeId], cancellationToken).ConfigureAwait(false);
    }

    private static Task<object> Push(TIn payload, IPipeEnter<TIn> enterPipe, CancellationToken cancellationToken)
    {
        var pipelineCompletionSource = new TaskCompletionSource<object?>();
        var context = new PipelineContext(pipelineCompletionSource, cancellationToken);

        enterPipe.Push(payload, context);

        return pipelineCompletionSource.Task!;
    }
}