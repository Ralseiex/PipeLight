using PipeLight.Abstractions.Context;

namespace PipeLight.Context;

public class PipelineContext : IPipelineContext
{
    public PipelineContext(TaskCompletionSource<object?> pipelineCompletionSource)
    {
        PipelineCompletionSource = pipelineCompletionSource;
    }

    public TaskCompletionSource<object?> PipelineCompletionSource { get; }
}