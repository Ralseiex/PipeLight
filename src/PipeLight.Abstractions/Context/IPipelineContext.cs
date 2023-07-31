namespace PipeLight.Abstractions.Context;

public interface IPipelineContext
{
    Guid CorrelationToken { get; }
    TaskCompletionSource<object?> PipelineCompletionSource { get; }
    CancellationToken CancellationToken { get; }
}
