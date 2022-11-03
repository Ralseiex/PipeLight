namespace PipeLight.Nodes.Steps.Interfaces;

public interface ISealedStep<T>
{
    Task ExecuteSealedStepAsync(T payload);
}
