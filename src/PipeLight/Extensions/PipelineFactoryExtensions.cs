using PipeLight.Interfaces;
using PipeLight.Nodes.Steps;
using PipeLight.Pipes;
using PipeLight.Pipes.Interfaces;

namespace PipeLight.Extensions;

public static class PipelineFactoryExtensions
{
    public static IPipe<T> CreatePipeline<T>(this PipelineFactory factory, Func<T, T> firstStepHandler)
    {
        var step = new FuncPipeStep<T>(firstStepHandler);
        return factory.CreatePipeline(step);
    }
    public static IPipeline<TIn, TOut> CreatePipeline<TIn, TOut>(this PipelineFactory factory, Func<TIn, TOut> firstStepHandler)
    {
        var node = new PipeTransformFunc<TIn, TOut>(firstStepHandler);
        return factory.CreatePipeline(node);
    }
}