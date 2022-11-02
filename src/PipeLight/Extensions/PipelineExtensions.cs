using PipeLight.Interfaces;
using PipeLight.Nodes;
using PipeLight.Nodes.Steps;
using PipeLight.Nodes.Steps.Interfaces;
using PipeLight.Pipes;

namespace PipeLight.Extensions;
public static class PipelineExtensions
{
    public static IPipeline<TIn, TNewOut> AddFitting<TIn, TOut, TNewOut>(this IPipeline<TIn, TOut> pipeline, Func<TOut, TNewOut> fittingHandler)
    {
        var fittingNode = new PipeFittingFunc<TOut, TNewOut>(fittingHandler);
        return pipeline.AddFitting(fittingNode);
    }
    public static ISealedPipeline<TIn> Seal<TIn, TOut>(this IPipeline<TIn, TOut> pipeline, Action<TOut> fittingHandler)
    {
        var sealedStep = new SealedActionStep<TOut>(fittingHandler);
        return pipeline.Seal(sealedStep);
    }

    public static ISealedPipeline<TIn> Seal<TIn, TOut>(this IPipeline<TIn, TOut> pipeline, ISealedStep<TOut> lastStep)
    {
        var pipeNode = new SealedPipe<TOut>(lastStep);
        return pipeline.Seal(pipeNode);
    }
}
