using PipeLight.Abstractions.Pipelines;
using PipeLight.Abstractions.Pipes;
using PipeLight.Context;
using PipeLight.Exceptions;

namespace PipeLight.Pipelines;

public class Pipeline<TIn, TOut> : IPipeline<TIn, TOut>
{
    private readonly IPipeEnter<TIn> _firstPipe;
    private readonly ReadOnlyPipesDictionary _pipes;

    public Pipeline(IPipeEnter<TIn> firstPipe, PipesDictionary pipes)
    {
        _firstPipe = firstPipe;
        _pipes = new ReadOnlyPipesDictionary(pipes);
    }

    public IEnumerable<string> PipesHashes => _pipes.Keys;

    public async Task<TOut> Push(TIn payload, CancellationToken cancellationToken = default)
    {
        return (TOut)await Push(payload, _firstPipe, cancellationToken).ConfigureAwait(false);
    }

    public async Task<TOut> PushToPipe(object payload, string pipeId, CancellationToken cancellationToken = default)
    {
        if (payload is null)
            throw new NullReferenceException(nameof(payload));

        if (!_pipes.ContainsKey(pipeId))
            throw new PipeNotFoundException();

        return (TOut)await Push(payload, _pipes[pipeId], cancellationToken);
    }

    private static Task<object> Push(TIn payload, IPipeEnter enterPipe, CancellationToken cancellationToken)
    {
        if (payload is null)
            throw new NullReferenceException(nameof(payload));

        var pipelineCompletionSource = new TaskCompletionSource<object?>();
        var context = new PipelineContext(Guid.NewGuid(), pipelineCompletionSource, cancellationToken);

        enterPipe.Push(payload, context);

        return pipelineCompletionSource.Task!;
    }

    private static Task<object> Push(object payload, IPipeEnter enterPipe, CancellationToken cancellationToken)
    {
        if (payload is null)
            throw new NullReferenceException(nameof(payload));

        var pipelineCompletionSource = new TaskCompletionSource<object?>();
        var context = new PipelineContext(Guid.NewGuid(), pipelineCompletionSource, cancellationToken);

        enterPipe.Push(payload, context);

        return pipelineCompletionSource.Task!;
    }
}