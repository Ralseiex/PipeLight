namespace PipeLight.Steps.Interfaces;

public interface IPipeStepHandler<T>
{
    Task<T> ExecuteAsync(T payload);
}
