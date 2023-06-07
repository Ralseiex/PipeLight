using PipeLight.Abstractions.Context;
using PipeLight.Abstractions.Pipes;
using PipeLight.Abstractions.Steps;

namespace PipeLight.Pipes;

internal class SealedPipe<T> : ISealedPipe<T>
{
    private readonly IPipelineSealedStep<T> _step;

    public SealedPipe(IPipelineSealedStep<T> step)
    {
        Id = Guid.NewGuid();
        _step = step;
    }

    public Guid Id { get; }

    public async Task Push(T payload, IPipelineContext context)
    {
        try
        {
            context.CancellationToken.ThrowIfCancellationRequested();
            await _step.Execute(payload).ConfigureAwait(false);
            context.PipelineCompletionSource.SetResult(null);
        }
        catch (Exception ex)
        {
            context.PipelineCompletionSource.SetException(ex);
        }
    }
}