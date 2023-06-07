using PipeLight.Abstractions.Pipelines;
using PipeLight.Abstractions.Pipes;
using PipeLight.Context;
using PipeLight.Exceptions;

namespace PipeLight.Pipelines;

public class SealedPipeline<T> : ISealedPipeline<T>
{
    private readonly IPipeEnter<T> _firstPipe;
    private readonly ReadOnlyPipesDictionary<T> _pipes;
    
    public SealedPipeline(IPipeEnter<T> firstPipe, PipesDictionary<T> pipes)
    {
        _firstPipe = firstPipe;
        _pipes = new ReadOnlyPipesDictionary<T>(pipes);
    }
    
    public async Task Push(T payload, CancellationToken cancellationToken = default)
    {
        await Push(payload, _firstPipe, cancellationToken).ConfigureAwait(false);
    }

    public async Task PushToPipe(T payload, Guid pipeId, CancellationToken cancellationToken = default)
    {
        if (!_pipes.ContainsKey(pipeId))
            throw new PipeNotFoundException();
        await Push(payload, _pipes[pipeId], cancellationToken).ConfigureAwait(false);
    }
    
    private static Task Push(T payload, IPipeEnter<T> enterPipe, CancellationToken cancellationToken)
    {
        var pipelineCompletionSource = new TaskCompletionSource<object?>();
        var context = new PipelineContext(pipelineCompletionSource, cancellationToken);

        enterPipe.Push(payload, context);

        return pipelineCompletionSource.Task;
    }
}