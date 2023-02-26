namespace PipeLight.Abstractions.Context;

public interface IPipelineContext
{
    TaskCompletionSource<object?> PipelineCompletionSource { get; }
}