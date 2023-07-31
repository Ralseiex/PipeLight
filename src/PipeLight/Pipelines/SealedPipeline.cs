using PipeLight.Abstractions.Pipelines;
using PipeLight.Abstractions.Pipes;
using PipeLight.Context;
using PipeLight.Exceptions;

namespace PipeLight.Pipelines;

public class SealedPipeline<TIn> : ISealedPipeline<TIn>
{
    private readonly IPipeEnter<TIn> _firstPipe;
    private readonly ReadOnlyPipesDictionary _pipes;

    public SealedPipeline(IPipeEnter<TIn> firstPipe, PipesDictionary pipes)
    {
        _firstPipe = firstPipe;
        _pipes = new ReadOnlyPipesDictionary(pipes);
    }

    public IEnumerable<string> PipesHashes => _pipes.Keys;

    public async Task Push(TIn payload, CancellationToken cancellationToken = default)
    {
        await Push(payload, _firstPipe, cancellationToken).ConfigureAwait(false);
    }

    public async Task PushToPipe(TIn payload, string pipeId, CancellationToken cancellationToken = default)
    {
        if (!_pipes.ContainsKey(pipeId))
            throw new PipeNotFoundException();
        await Push(payload, _pipes[pipeId], cancellationToken).ConfigureAwait(false);
    }

    public Task PushToPipe(object payload, string pipeId, CancellationToken cancellationToken = default)
    {
        if (payload is not TIn typedPayload)
            throw new InvalidPayloadTypeException();
        return PushToPipe(typedPayload, pipeId, cancellationToken);
    }

    private static Task Push(TIn payload, IPipeEnter enterPipe, CancellationToken cancellationToken)
    {
        if (payload is null)
            throw new NullReferenceException(nameof(payload));

        var pipelineCompletionSource = new TaskCompletionSource<object?>();
        var context = new PipelineContext(Guid.NewGuid(), pipelineCompletionSource, cancellationToken);

        enterPipe.Push(payload, context);

        return pipelineCompletionSource.Task;
    }
}