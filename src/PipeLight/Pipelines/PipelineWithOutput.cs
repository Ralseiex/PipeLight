using PipeLight.Abstractions.Pipelines;
using PipeLight.Abstractions.Pipes;

namespace PipeLight.Pipelines;

public sealed class PipelineWithOutput<TIn, TOut> : PipelineCore<TIn>, IPipelineWithOutput<TIn, TOut>
{
    public PipelineWithOutput(IPipeEnter<TIn> firstPipe, PipesDictionary pipes) : base(firstPipe)
    {
    }

    public async Task<TOut> Push(TIn payload, CancellationToken cancellationToken = default)
    {
        return (TOut)await Push(payload, FirstPipe, cancellationToken).ConfigureAwait(false);
    }
}