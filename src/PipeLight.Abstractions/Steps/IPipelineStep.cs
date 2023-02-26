namespace PipeLight.Abstractions.Steps;

public interface IPipelineStep<T>
{
    Task<T> Execute(T payload);
}