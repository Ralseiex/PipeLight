using PipeLight.Abstractions.Pipelines;
using PipeLight.Abstractions.Pipes;
using PipeLight.Abstractions.Steps;
using PipeLight.Context;

namespace PipeLight.Pipelines;

public class Pipeline<T> : IPipeline<T>
{
    private readonly IEnumerable<IPipelineStep<T>> _steps;

    public Pipeline(IEnumerable<IPipelineStep<T>> steps)
    {
        _steps = steps;
    }

    public async Task<T> Push(T payload)
    {
        var result = payload;
        foreach (var step in _steps)
        {
            result = await step.Execute(result);
        }

        return result;
    }
}

public class Pipeline<TIn, TOut> : IPipeline<TIn, TOut>
{
    private readonly IPipeEnter<TIn> _firstPipe;

    public Pipeline(IPipeEnter<TIn> firstPipe)
    {
        _firstPipe = firstPipe;
    }

    public async Task<TOut> Push(TIn payload)
    {
        var pipelineCompletionSource = new TaskCompletionSource<object?>();
        var context = new PipelineContext(pipelineCompletionSource);

        await _firstPipe.Push(payload, context).ConfigureAwait(false);

        return (TOut)(await pipelineCompletionSource.Task)!;
    }
}