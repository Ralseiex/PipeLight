using PipeLight.Steps.Interfaces;

namespace PipeLight.Steps;

public class PipeStep<T> : IPipeStep<T>
{
    private readonly IPipeStepHandler<T> _handler;

    public PipeStep(IPipeStepHandler<T> handler)
    {
        _handler = handler;
    }

    public async Task<T> ExecuteStepAsync(T payload)
    {
        return await _handler.ExecuteAsync(payload);
    }
}
