using PipeLight.Abstractions.Context;

namespace PipeLight.Context;

public record PipelineContext(TaskCompletionSource<object?> PipelineCompletionSource) : IPipelineContext;