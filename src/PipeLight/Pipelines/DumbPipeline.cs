using PipeLight.Abstractions.Pipelines;

namespace PipeLight.Pipelines;

public sealed class DumbPipeline<TIn> : IPipeline<TIn>
{
    public Task Push(TIn payload, CancellationToken cancellationToken = default) => Task.CompletedTask;
}