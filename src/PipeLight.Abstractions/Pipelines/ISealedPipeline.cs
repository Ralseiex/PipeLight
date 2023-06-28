namespace PipeLight.Abstractions.Pipelines;

public interface ISealedPipeline
{
    Task PushToPipe(object payload, Guid pipeId, CancellationToken cancellationToken = default);
}

public interface ISealedPipeline<in TIn> : ISealedPipeline
{
    Task Push(TIn payload, CancellationToken cancellationToken = default);
    Task PushToPipe(TIn payload, Guid pipeId, CancellationToken cancellationToken = default);
}
