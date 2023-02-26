using PipeLight.Abstractions.Context;
using PipeLight.Abstractions.Pipes;
using PipeLight.Abstractions.Steps;

namespace PipeLight.Pipes;

internal class TransformPipe<TIn, TOut> : ITransformPipe<TIn, TOut>
{
    private readonly IPipelineTransform<TIn, TOut> _transform;

    public TransformPipe(IPipelineTransform<TIn, TOut> transform)
    {
        _transform = transform;
    }

    public IPipeEnter<TOut>? NextPipe { get; set; }

    public async Task Push(TIn payload, IPipelineContext context)
    {
        try
        {
            var result = await _transform.Transform(payload).ConfigureAwait(false);

            if (NextPipe is not null)
                await NextPipe.Push(result, context).ConfigureAwait(false);
            else
                context.PipelineCompletionSource.SetResult(result);
        }
        catch (Exception ex)
        {
            context.PipelineCompletionSource.SetException(ex);
        }
    }
}