using PipeLight.Abstractions.Pipes;
using PipeLight.Exceptions;

namespace PipeLight.Pipelines;

public class Pipeline<TIn, TOut> : PipelineBase<TIn, TOut>
{
    public Pipeline(IPipeEnter<TIn> firstPipe, PipesDictionary pipes) : base(firstPipe, pipes)
    {
    }

    public override async Task<TOut> Push(TIn payload, CancellationToken cancellationToken = default)
    {
        return (TOut)await Push(payload, FirstPipe, cancellationToken).ConfigureAwait(false);
    }

    public override async Task<TOut> PushToPipe(
        object payload,
        string pipeId,
        CancellationToken cancellationToken = default)
    {
        if (payload is null)
            throw new NullReferenceException(nameof(payload));

        if (!Pipes.ContainsKey(pipeId))
            throw new PipeNotFoundException();

        return (TOut)await Push(payload, Pipes[pipeId], cancellationToken);
    }
}