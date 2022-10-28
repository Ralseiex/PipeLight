using PipeLight.Steps.Interfaces;

namespace PipeLight.Steps.Delegates;

internal class FuncStepAsync<T> : IPipeStepHandler<T>
{
    private readonly Func<T, T> _handler;

    public FuncStepAsync(Func<T, T> handler)
    {
        _handler = handler;
    }

    public async Task<T> ExecuteAsync(T payload)
        => await Task.FromResult(_handler(payload));
}




