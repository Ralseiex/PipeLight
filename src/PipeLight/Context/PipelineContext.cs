using PipeLight.Abstractions.Context;

namespace PipeLight.Context;

public class PipelineContext : IPipelineContext
{
    public PipelineContext(
        TaskCompletionSource<object?> pipelineCompletionSource,
        CancellationToken cancellationToken)
    {
        PipelineCompletionSource = pipelineCompletionSource;
        CancellationToken = cancellationToken;
    }

    public TaskCompletionSource<object?> PipelineCompletionSource { get; }
    public CancellationToken CancellationToken { get; }
}