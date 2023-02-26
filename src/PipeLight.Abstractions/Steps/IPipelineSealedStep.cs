namespace PipeLight.Abstractions.Steps;

public interface IPipelineSealedStep<in T>
{
    Task Execute(T payload);
}