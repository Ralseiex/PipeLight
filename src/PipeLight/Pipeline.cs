using PipeLight.Base;
using PipeLight.Interfaces;
using PipeLight.Nodes;
using PipeLight.Nodes.Interfaces;
using PipeLight.Pipes.Interfaces;

namespace PipeLight;

public class Pipeline<TIn> : PipelineBase<TIn, TIn>
{
    public Pipeline(IPipe<TIn> pipe)
    {
        var firstNode = new PipeNode<TIn>(pipe);
        FirstStep = firstNode;
        LastStep = firstNode;
    }
    public Pipeline(PipeNode<TIn> firstNode)
        : base(firstNode, firstNode)
    {
    }
    public Pipeline(IPipelineEnter<TIn> firstStep, IPipelineExit<TIn> secondNode)
        : base(firstStep, secondNode)
    {
    }

    public IPipeline<TIn, TIn> AddPipe<TNewOut>(IPipe<TIn> pipe)
    {
        var pipeNode = new PipeNode<TIn>(pipe);
        LastStep.NextNode = pipeNode;

        return new Pipeline<TIn>(FirstStep, pipeNode);
    }
    public IPipeline<TIn, TNewOut> AddFitting<TNewOut>(Func<TIn, TNewOut> fitting)
    {
        var fittingNode = new FittingNode<TIn, TNewOut>(fitting);
        LastStep.NextNode = fittingNode;

        return new Pipeline<TIn, TNewOut>(FirstStep, fittingNode);
    }
}
public class Pipeline<TIn, TOut> : PipelineBase<TIn, TOut>
{
    public Pipeline(FittingNode<TIn, TOut> firstNode)
        : base(firstNode, firstNode)
    {
    }
    public Pipeline(PipeNode<TIn> firstNode, FittingNode<TIn, TOut> secondNode)
        : base(firstNode, secondNode)
    {
    }
    public Pipeline(IPipelineEnter<TIn> firstStep, IPipelineExit<TOut> secondNode)
        : base(firstStep, secondNode)
    {
    }
}
