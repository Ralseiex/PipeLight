namespace PipeLight.Abstractions.Steps;

public interface IPipelineStep
{
}

public interface IPipelineStep<T> : IPipelineStep
{
    Task<T> Execute(T payload);
}