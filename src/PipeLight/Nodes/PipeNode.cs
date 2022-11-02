using PipeLight.Interfaces;
using PipeLight.Nodes.Interfaces;
using PipeLight.Nodes.Steps.Interfaces;
using PipeLight.Pipes;
using PipeLight.Pipes.Interfaces;

namespace PipeLight.Nodes;


public class PipeNode<TIn> : IPipelineEnter<TIn>, IPipelineExit<TIn>
{
    public IPipelineEnter<TIn>? NextNode { get; set; }
    public IPipe<TIn> Pipe { get; }

    public PipeNode(IPipe<TIn> pipe)
    {
        Pipe = pipe;
    }

    public PipeNode(IPipeStep<TIn> step)
    {
        var pipe = new Pipe<TIn>(step);
        Pipe = pipe;
    }

    public async Task PushAsync(TIn? payload, IPipelineContext context)
    {
        try
        {
            payload = await Pipe.PushAsync(payload).ConfigureAwait(false);

            if (NextNode is not null)
                _ = NextNode.PushAsync(payload, context).ConfigureAwait(false);
            else
                context.PipelineCompletionSource?.SetResult(payload);
        }
        catch (Exception ex)
        {
            context.PipelineCompletionSource?.SetException(ex);
        }
    }
}
