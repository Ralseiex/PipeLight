using PipeLight.Interfaces;
using PipeLight.Nodes.Interfaces;
using PipeLight.Pipes;
using PipeLight.Pipes.Interfaces;

namespace PipeLight.Nodes;

public class TransformNode<TIn, TOut> : IPipelineEnter<TIn>, IPipelineExit<TOut>
{
    private readonly IPipeTransform<TIn, TOut> _transform;
    public IPipelineEnter<TOut>? NextNode { get; set; }

    public TransformNode(IPipeTransform<TIn, TOut> transform)
    {
        _transform = transform;
    }
    public TransformNode(Func<TIn, TOut> transform)
    {
        _transform = new PipeTransformFunc<TIn, TOut>(transform);
    }

    public async Task PushAsync(TIn? payload, IPipelineContext context)
    {
        try
        {
            var result = await _transform.TransformAsync(payload).ConfigureAwait(false);

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