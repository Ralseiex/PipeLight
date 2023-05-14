namespace PipeLight.Abstractions.Context;

public interface IPipelineContext
{
    TaskCompletionSource<object?> PipelineCompletionSource { get; }
    CancellationToken CancellationToken { get; }
}