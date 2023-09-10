namespace PipeLight.Abstractions.Steps;

public interface IPipelineStep
{
}

public interface IPipelineAction<T> : IPipelineStep
{
    Task<T> Execute(T payload);
}