namespace PipeLight.Abstractions.Pipelines;

public interface IPipeline<in TIn, TOut>
{
    Task<TOut> Push(TIn payload, CancellationToken cancellationToken = default);
    Task<TOut> PushToPipe(TIn payload, Guid pipeId, CancellationToken cancellationToken = default);
}

public interface IPipeline<T> : IPipeline<T, T>
{
}