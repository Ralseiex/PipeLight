using PipeLight.Nodes.Steps.Interfaces;

namespace PipeLight.Nodes.Steps;

internal class SealedActionStep<TIn> : ISealedStep<TIn>
{
    private readonly Action<TIn> _handler;

    public SealedActionStep(Action<TIn> handler)
    {
        _handler = handler;
    }

    public async Task ExecuteSealedStepAsync(TIn payload)
    {
        await Task.Run(() => _handler(payload));
    }
}




