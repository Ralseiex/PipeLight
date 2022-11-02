using PipeLight.Interfaces;
using PipeLight.Nodes.Steps.Interfaces;

namespace PipeLight.Pipes.Interfaces;

public interface IPipe
{
    IPipe<T> AddStep<T>(IPipeStep<T> firstStep);
    IPipeline<T, TNewOut> AddFitting<T, TNewOut>(IPipeFitting<T, TNewOut> pipeFitting);
    IPipeline<T, T> AddPipe<T>(IPipe<T> pipe);
    ISealedPipe<T> Seal<T>(ISealedStep<T> firstAndLastStep);
}

public interface IPipe<T>
{
    IEnumerable<IPipeStep<T>> Steps { get; }

    IPipe<T> AddStep(IPipeStep<T> pipeStep);
    IPipeline<T, TNewOut> AddFitting<TNewOut>(IPipeFitting<T, TNewOut> pipeFitting);
    ISealedPipe<T> Seal(ISealedStep<T> firstAndLastStep);
    Task<T> PushAsync(T payload);
}
