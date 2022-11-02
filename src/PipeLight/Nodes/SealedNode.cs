using PipeLight.Interfaces;
using PipeLight.Nodes.Interfaces;
using PipeLight.Nodes.Steps.Interfaces;
using PipeLight.Pipes;
using PipeLight.Pipes.Interfaces;

namespace PipeLight.Nodes;

public class SealedNode<TIn> : IPipelineEnter<TIn>
{
    public ISealedPipe<TIn> Pipe { get; }
    public SealedNode(ISealedPipe<TIn> pipe)
    {
        Pipe = pipe;
    }

    public SealedNode(ISealedStep<TIn> step)
    {
        var pipe = new SealedPipe<TIn>(step);
        Pipe = pipe;
    }
    public async Task PushAsync(TIn payload, IPipelineContext context)
    {
        try
        {
            await Pipe.PushAsync(payload).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            context.PipelineCompletionSource?.SetException(ex);
        }
    }
}