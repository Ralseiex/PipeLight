namespace PipeLight.Interfaces;

public interface IPipelineContext
{
    TaskCompletionSource<object?> PipelineCompletionSource { get; }
}