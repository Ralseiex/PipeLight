using PipeLight.Abstractions.Context;

namespace PipeLight.Context;

public class PipelineContext : IPipelineContext
{
    public PipelineContext(
        Guid correlationToken,
        TaskCompletionSource<object?> pipelineCompletionSource,
        CancellationToken cancellationToken)
    {
        CorrelationToken = correlationToken;
        PipelineCompletionSource = pipelineCompletionSource;
        CancellationToken = cancellationToken;
    }

    public Guid CorrelationToken { get; }
    public TaskCompletionSource<object?> PipelineCompletionSource { get; }
    public CancellationToken CancellationToken { get; }
}
