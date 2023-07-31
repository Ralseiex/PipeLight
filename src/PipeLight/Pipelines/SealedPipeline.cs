using PipeLight.Abstractions.Pipes;
using PipeLight.Exceptions;

namespace PipeLight.Pipelines;

public class SealedPipeline<TIn> : SealedPipelineBase<TIn>
{
    public SealedPipeline(IPipeEnter<TIn> firstPipe, PipesDictionary pipes) : base(firstPipe, pipes)
    {
    }

    public override async Task Push(TIn payload, CancellationToken cancellationToken = default)
    {
        await Push(payload, FirstPipe, cancellationToken).ConfigureAwait(false);
    }

    public override async Task PushToPipe(object payload, string pipeId, CancellationToken cancellationToken = default)
    {
        if (payload is null)
            throw new NullReferenceException(nameof(payload));

        if (!Pipes.ContainsKey(pipeId))
            throw new PipeNotFoundException();

        await Push(payload, Pipes[pipeId], cancellationToken);
    }
}