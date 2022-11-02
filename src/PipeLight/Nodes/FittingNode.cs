using PipeLight.Interfaces;
using PipeLight.Nodes.Interfaces;
using PipeLight.Pipes;
using PipeLight.Pipes.Interfaces;

namespace PipeLight.Nodes;

public class FittingNode<TIn, TOut> : IPipelineEnter<TIn>, IPipelineExit<TOut>
{
    private readonly IPipeFitting<TIn, TOut> _fitting;
    public IPipelineEnter<TOut>? NextNode { get; set; }

    public FittingNode(IPipeFitting<TIn, TOut> fitting)
    {
        _fitting = fitting;
    }
    public FittingNode(Func<TIn, TOut> fitting)
    {
        _fitting = new PipeFittingFunc<TIn, TOut>(fitting);
    }

    public async Task PushAsync(TIn? payload, IPipelineContext context)
    {
        try
        {
            var result = await _fitting.FitAsync(payload).ConfigureAwait(false);

            if (NextNode is not null)
                _ = NextNode.PushAsync(result, context).ConfigureAwait(false);
            else
                context.PipelineCompletionSource?.SetResult(result);
        }
        catch (Exception ex)
        {
            context.PipelineCompletionSource?.SetException(ex);
        }
    }
}