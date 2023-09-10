namespace PipeLight.Abstractions.Steps;

public interface IPipelineSeal<in T> : IPipelineStep
{
    Task Execute(T payload);
}