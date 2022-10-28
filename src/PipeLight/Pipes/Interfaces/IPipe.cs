using PipeLight.Interfaces.Steps;
using PipeLight.Steps.Interfaces;

namespace PipeLight.Pipes.Interfaces;

public interface IPipe<T>
{
    Task<T> PushAsync(T payload);
    IPipe<T> AddStep(IPipeStep<T> pipeStep);
}
