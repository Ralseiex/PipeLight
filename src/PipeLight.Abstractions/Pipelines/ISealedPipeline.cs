namespace PipeLight.Abstractions.Pipelines;

public interface ISealedPipeline<in TIn>
{
    Task Push(TIn payload, CancellationToken cancellationToken = default);
}