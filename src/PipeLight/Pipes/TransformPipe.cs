using PipeLight.Abstractions.Context;
using PipeLight.Abstractions.Pipes;
using PipeLight.Abstractions.Steps;

namespace PipeLight.Pipes;

internal class TransformPipe<TIn, TOut> : ITransformPipe<TIn, TOut>
{
    private readonly IPipelineTransformStep<TIn, TOut> _transformStep;

    public TransformPipe(IPipelineTransformStep<TIn, TOut> transformStep)
    {
        _transformStep = transformStep;
    }

    public IPipeEnter<TOut>? NextPipe { get; set; }

    public async Task Push(TIn payload, IPipelineContext context)
    {
        try
        {
            var result = await _transformStep.Transform(payload).ConfigureAwait(false);

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