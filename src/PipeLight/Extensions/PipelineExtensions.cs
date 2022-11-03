using PipeLight.Interfaces;
using PipeLight.Nodes;
using PipeLight.Nodes.Steps;
using PipeLight.Nodes.Steps.Interfaces;
using PipeLight.Pipes;

namespace PipeLight.Extensions;
public static class PipelineExtensions
{
    public static IPipeline<TIn, TOut> AddStep<TIn, TOut>(this IPipeline<TIn, TOut> pipeline, Func<TOut, TOut> stepHandler)
    {
        var step = new FuncPipeStep<TOut>(stepHandler);
        return pipeline.AddStep(step);
    }
    public static IPipeline<TIn, TNewOut> AddTransform<TIn, TOut, TNewOut>(this IPipeline<TIn, TOut> pipeline, Func<TOut, TNewOut> transformHandler)
    {
        var transformNode = new PipeTransformFunc<TOut, TNewOut>(transformHandler);
        return pipeline.AddTransform(transformNode);
    }
    public static ISealedPipeline<TIn> Seal<TIn, TOut>(this IPipeline<TIn, TOut> pipeline, Action<TOut> sealHandler)
    {
        var sealedStep = new SealedActionStep<TOut>(sealHandler);
        return pipeline.Seal(sealedStep);
    }
    public static ISealedPipeline<TIn> Seal<TIn, TOut>(this IPipeline<TIn, TOut> pipeline, ISealedStep<TOut> lastStep)
    {
        var pipeNode = new SealedPipe<TOut>(lastStep);
        return pipeline.Seal(pipeNode);
    }
}
