namespace PipeLight.Abstractions.Pipelines;

public interface IPipelineWithOutput<in TIn, TOut>
{
    Task<TOut> Push(TIn payload, CancellationToken cancellationToken = default);
}