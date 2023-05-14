namespace PipeLight.Abstractions.Pipelines;

public interface IPipeline<in TIn, TOut>
{
    Task<TOut> Push(TIn payload, CancellationToken cancellationToken = default);
}

public interface IPipeline<T> : IPipeline<T, T>
{
}