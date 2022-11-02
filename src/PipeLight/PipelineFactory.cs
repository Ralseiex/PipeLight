using PipeLight.Interfaces;
using PipeLight.Nodes;
using PipeLight.Nodes.Steps;
using PipeLight.Nodes.Steps.Interfaces;
using PipeLight.Pipes;
using PipeLight.Pipes.Interfaces;

namespace PipeLight;

public class PipelineFactory
{
    public IPipe CreatePipeline()
    {
        return new Pipe();
    }
    public IPipe<T> CreatePipeline<T>(IPipeStep<T> firstStep)
    {
        return new Pipe<T>(firstStep);
    }
    public ISealedPipe<T> CreatePipeline<T>(ISealedStep<T> firstAndLastStep)
    {
        return new SealedPipe<T>(firstAndLastStep);
    }
    public IPipeline<TIn, TOut> CreatePipeline<TIn, TOut>(IPipeFitting<TIn, TOut> firstStep)
    {
        var node = new FittingNode<TIn, TOut>(firstStep);
        return new Pipeline<TIn, TOut>(node);
    }
}
