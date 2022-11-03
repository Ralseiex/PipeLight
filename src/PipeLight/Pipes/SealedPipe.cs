using PipeLight.Nodes.Steps.Interfaces;
using PipeLight.Pipes.Interfaces;

namespace PipeLight.Pipes;

public class SealedPipe<T> : ISealedPipe<T>
{
    private readonly List<IPipeStep<T>> _steps = new();
    private readonly ISealedStep<T> _lastStep;

    public SealedPipe(ISealedStep<T> lastStep)
    {
        _lastStep = lastStep;
    }
    public SealedPipe(IPipe<T> pipe, ISealedStep<T> lastStep)
    {
        _steps = new(pipe.Steps);
        _lastStep = lastStep;
    }

    public async Task PushAsync(T payload)
    {
        var result = payload;
        foreach (var step in _steps)
        {
            result = await step.ExecuteStepAsync(result);
        }
        await _lastStep.ExecuteSealedStepAsync(result);
    }
}