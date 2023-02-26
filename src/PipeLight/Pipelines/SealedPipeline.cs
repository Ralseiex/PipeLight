using PipeLight.Abstractions.Pipelines;
using PipeLight.Abstractions.Pipes;
using PipeLight.Context;

namespace PipeLight.Pipelines;

public class SealedPipeline<T> : ISealedPipeline<T>
{
    private readonly IPipeEnter<T> _firstPipe;


    public SealedPipeline(IPipeEnter<T> firstPipe)
    {
        _firstPipe = firstPipe;
    }


    public async Task Push(T payload)
    {
        var pipelineCompletionSource = new TaskCompletionSource<object?>();
        var context = new PipelineContext(pipelineCompletionSource);

        await _firstPipe.Push(payload, context).ConfigureAwait(false);
        await pipelineCompletionSource.Task;
    }
}