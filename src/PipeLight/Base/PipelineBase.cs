using PipeLight.Interfaces;
using PipeLight.Nodes;
using PipeLight.Nodes.Interfaces;
using PipeLight.Nodes.Steps.Interfaces;
using PipeLight.Pipes;
using PipeLight.Pipes.Interfaces;

namespace PipeLight.Base;

public abstract class PipelineBase<TIn> : ISealedPipeline<TIn>
{
    public PipelineBase(IPipelineEnter<TIn> firstStep)
    {
        FirstStep = firstStep;
    }


    public IPipelineEnter<TIn> FirstStep { get; init; }


    public async Task PushAsync(TIn data)
    {
        var pipelineCompletionSource = new TaskCompletionSource<object>();
        var task = pipelineCompletionSource.Task;
        var context = new PipelineContext(pipelineCompletionSource);

        FirstStep?.PushAsync(data, context).ConfigureAwait(false);

        await task;
    }
}
public abstract class PipelineBase<TIn, TOut> : IPipeline<TIn, TOut>
{
    protected PipelineBase() { }
    public PipelineBase(IPipelineEnter<TIn> firstStep, IPipelineExit<TOut> lastStep)
    {
        FirstStep = firstStep;
        LastStep = lastStep;
    }


    public IPipelineEnter<TIn> FirstStep { get; init; }
    public IPipelineExit<TOut> LastStep { get; init; }


    public async Task<TOut> PushAsync(TIn data)
    {
        var pipelineCompletionSource = new TaskCompletionSource<object>();
        var task = pipelineCompletionSource.Task;
        var context = new PipelineContext(pipelineCompletionSource);

        FirstStep?.PushAsync(data, context).ConfigureAwait(false);

        return (TOut)(await task);
    }

    public IPipeline<TIn, TOut> AddPipe(IPipe<TOut> pipeTransform)
    {
        var pipeNode = new PipeNode<TOut>(pipeTransform);

        LastStep.NextNode = pipeNode;
        return new Pipeline<TIn, TOut>(FirstStep, pipeNode);
    }
    public IPipeline<TIn, TOut> AddStep(IPipeStep<TOut> step)
    {
        if (LastStep is PipeNode<TOut> node)
        {
            node.Pipe.AddStep(step);
            return this;
        }
        var newNode = new PipeNode<TOut>(step);

        LastStep.NextNode = newNode;
        return new Pipeline<TIn, TOut>(FirstStep, newNode);
    }
    public IPipeline<TIn, TNewOut> AddTransform<TNewOut>(IPipeTransform<TOut, TNewOut> pipeTransform)
    {
        var transformNode = new TransformNode<TOut, TNewOut>(pipeTransform);

        LastStep.NextNode = transformNode;
        return new Pipeline<TIn, TNewOut>(FirstStep, transformNode);
    }

    public ISealedPipeline<TIn> Seal(ISealedPipe<TOut> lastStep)
    {
        var pipeNode = new SealedNode<TOut>(lastStep);

        LastStep.NextNode = pipeNode;
        return new SealedPipeline<TIn>(FirstStep);
    }
}
