namespace PipeLight.Abstractions.Pipelines;

public interface IPipeline<TOut>
{
    Task<TOut> PushToPipe(object payload, Guid pipeId, CancellationToken cancellationToken = default);
}
public interface IPipeline<in TIn, TOut> : IPipeline<TOut>
{
    Task<TOut> Push(TIn payload, CancellationToken cancellationToken = default);
}
