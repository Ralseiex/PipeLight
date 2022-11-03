using PipeLight.Interfaces;

namespace PipeLight;

public record PipelineContext(TaskCompletionSource<object?> PipelineCompletionSource) : IPipelineContext;