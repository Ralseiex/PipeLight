using PipeLight.Abstractions.Context;
using PipeLight.Abstractions.Pipes;
using PipeLight.Abstractions.Steps;

namespace PipeLight.Pipes;

public class TransformPipe<TIn, TOut> : ITransformPipe<TIn, TOut>
{
    private readonly IPipelineTransform<TIn, TOut> _transform;

    public TransformPipe(IPipelineTransform<TIn, TOut> transform)
    {
        Id = Guid.NewGuid();
        _transform = transform;
    }

    public Guid Id { get; }
    public IPipeEnter<TOut>? NextPipe { get; set; }

    public async Task Push(TIn payload, IPipelineContext context)
    {
        try
        {
            context.CancellationToken.ThrowIfCancellationRequested();
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