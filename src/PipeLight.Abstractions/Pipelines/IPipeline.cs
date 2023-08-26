namespace PipeLight.Abstractions.Pipelines;

public interface IPipeline<in TIn>
{
    Task Push(TIn payload, CancellationToken cancellationToken = default);
}