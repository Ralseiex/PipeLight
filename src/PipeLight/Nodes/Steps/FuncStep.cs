using PipeLight.Nodes.Steps.Interfaces;

namespace PipeLight.Nodes.Steps;

internal class FuncPipeStep<T> : IPipeStep<T>
{
    private readonly Func<T, T> _handler;

    public FuncPipeStep(Func<T, T> handler)
    {
        _handler = handler;
    }

    public async Task<T> ExecuteStepAsync(T payload)
        => await Task.FromResult(_handler(payload));
}




