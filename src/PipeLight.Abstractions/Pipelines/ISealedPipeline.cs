namespace PipeLight.Abstractions.Pipelines;

public interface ISealedPipeline<in TIn>
{
    Task Push(TIn payload, CancellationToken cancellationToken = default);
    Task PushToPipe(TIn payload, Guid pipeId, CancellationToken cancellationToken = default);
}