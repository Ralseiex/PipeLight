using PipeLight.Steps.Interfaces;

namespace PipeLight.Steps.Delegates;

internal class ActionSealAsync<TIn> : IPipelineStepAsyncHandler<TIn>
{
    private readonly Action<TIn> _handler;

    public ActionSealAsync(Action<TIn> handler)
    {
        _handler = handler;
    }
    public async Task InvokeAsync(TIn payload)
        => await Task.FromResult(() => _handler(payload));
}




