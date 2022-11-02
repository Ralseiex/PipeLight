using PipeLight.Interfaces;
using PipeLight.Nodes.Steps.Interfaces;

namespace PipeLight.Pipes.Interfaces;

public interface IPipe
{
    IPipe<T> AddStep<T>(IPipeStep<T> firstStep);
    IPipeline<T, TNewOut> AddTransform<T, TNewOut>(IPipeTransform<T, TNewOut> pipeTransform);
    IPipeline<T, T> AddPipe<T>(IPipe<T> pipe);
    ISealedPipe<T> Seal<T>(ISealedStep<T> firstAndLastStep);
}

public interface IPipe<T>
{
    IEnumerable<IPipeStep<T>> Steps { get; }

    IPipe<T> AddStep(IPipeStep<T> pipeStep);
    IPipeline<T, TNewOut> AddTransform<TNewOut>(IPipeTransform<T, TNewOut> pipeTransform);
    ISealedPipe<T> Seal(ISealedStep<T> firstAndLastStep);
    Task<T> PushAsync(T payload);
}
