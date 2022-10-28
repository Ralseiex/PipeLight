using PipeLight.Pipes.Interfaces;
using PipeLight.Steps.Interfaces;

namespace PipeLight.Pipes;

public class Pipe
{
    public IPipe<T> AddStep<T>(IPipeStep<T> pipeStep)
    {
        var pipe = new Pipe<T>();
        pipe.AddStep(pipeStep);
        return pipe;
    }
}
public class Pipe<T> : IPipe<T>
{
    private readonly List<IPipeStep<T>> _steps = new();
    public IPipe<T> AddStep(IPipeStep<T> pipeStep)
    {
        _steps.Add(pipeStep);
        return this;
    }

    public async Task<T> PushAsync(T payload)
    {
        var result = payload;
        foreach (var step in _steps)
        {
            result = await step.ExecuteStepAsync(result);
        }
        return result;
    }
}
