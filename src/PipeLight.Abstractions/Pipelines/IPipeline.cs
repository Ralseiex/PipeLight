namespace PipeLight.Abstractions.Pipelines;

public interface IPipeline<in TIn, TOut>
{
    Task<TOut> Push(TIn payload);
}

public interface IPipeline<T> : IPipeline<T, T>
{
}