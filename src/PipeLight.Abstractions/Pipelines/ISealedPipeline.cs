namespace PipeLight.Abstractions.Pipelines;

public interface ISealedPipeline
{
    IEnumerable<string> PipesHashes { get; }
    Task PushToPipe(object payload, string pipeId, CancellationToken cancellationToken = default);
}

public interface ISealedPipeline<in TIn> : ISealedPipeline
{
    Task Push(TIn payload, CancellationToken cancellationToken = default);
}