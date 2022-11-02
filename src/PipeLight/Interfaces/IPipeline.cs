using PipeLight.Nodes.Steps.Interfaces;
using PipeLight.Pipes.Interfaces;

namespace PipeLight.Interfaces;

public interface IPipeline<TIn, TOut>
{
    Task<TOut> PushAsync(TIn payload);
    IPipeline<TIn, TOut> AddPipe(IPipe<TOut> pipe);
    IPipeline<TIn, TOut> AddStep(IPipeStep<TOut> pipeStep);
    IPipeline<TIn, TNewOut> AddFitting<TNewOut>(IPipeFitting<TOut, TNewOut> fitting);
    ISealedPipeline<TIn> Seal(ISealedPipe<TOut> lastStep);
}
