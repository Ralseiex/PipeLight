using PipeLight.Abstractions.Pipelines;
using PipeLight.Abstractions.Pipes;

namespace PipeLight.Pipelines;

public class Pipeline<TIn> : PipelineCore<TIn>, IPipeline<TIn>
{
    public Pipeline(IPipeEnter<TIn> firstPipe) : base(firstPipe)
    {
    }

    public async Task Push(TIn payload, CancellationToken cancellationToken = default)
    {
        await Push(payload, FirstPipe, cancellationToken).ConfigureAwait(false);
    }
}