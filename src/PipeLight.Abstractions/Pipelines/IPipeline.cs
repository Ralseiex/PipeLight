namespace PipeLight.Abstractions.Pipelines;

public interface IPipeline<TOut>
{
    IEnumerable<string> PipesHashes { get; }
    Task<TOut> PushToPipe(object payload, string pipeId, CancellationToken cancellationToken = default);
}

public interface IPipeline<in TIn, TOut> : IPipeline<TOut>
{
    Task<TOut> Push(TIn payload, CancellationToken cancellationToken = default);
}