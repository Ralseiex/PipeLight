namespace PipeLight.Abstractions.Steps;

public interface IPipelineSealedStep<in T> : IPipelineStep
{
    Task Execute(T payload);
}