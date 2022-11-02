using PipeLight.Interfaces;
using PipeLight.Nodes;
using PipeLight.Nodes.Steps;
using PipeLight.Nodes.Steps.Interfaces;
using PipeLight.Pipes;
using PipeLight.Pipes.Interfaces;

namespace PipeLight.Extensions;

public static class PipeExtensions
{
    public static IPipe<T> AddStep<T>(this IPipe pipe, Func<T, T> firstStepHandler)
    {
        var newPipe = new Pipe<T>();
        IPipeStep<T> newStep = new FuncPipeStep<T>(firstStepHandler);
        newPipe.AddStep(newStep);
        return newPipe;
    }
    public static IPipeline<T, TNewOut> AddTransform<T, TNewOut>(this IPipe pipe, Func<T, TNewOut> transformHandler)
    {
        var firstNode = new TransformNode<T, TNewOut>(transformHandler);
        var pipeline = new Pipeline<T, TNewOut>(firstNode);
        return pipeline;
    }
    public static IPipe<T> AddStep<T>(this IPipe<T> pipe, Func<T, T> pipeStepHandler)
    {
        IPipeStep<T> newStep = new FuncPipeStep<T>(pipeStepHandler);
        return pipe.AddStep(newStep);
    }
    public static IPipeline<T, TNewOut> AddTransform<T, TNewOut>(this IPipe<T> pipe, Func<T, TNewOut> transformHandler)
    {
        var pipeline = new Pipeline<T>(pipe)
            .AddTransform(transformHandler);
        return pipeline;
    }
}
