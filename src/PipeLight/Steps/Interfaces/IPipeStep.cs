namespace PipeLight.Steps.Interfaces;

public interface IPipeStep<T>
{
    Task<T> ExecuteStepAsync(T payload);
}
