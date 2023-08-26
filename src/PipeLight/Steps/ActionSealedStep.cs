using PipeLight.Abstractions.Steps;

namespace PipeLight.Steps;

internal class ActionSealedStep<TIn> : IPipelineSealedStep<TIn>
{
    private readonly Action<TIn> _handler;

    public ActionSealedStep(Action<TIn> handler) => _handler = handler;

    public async Task Execute(TIn payload) => await Task.Run(() => _handler(payload));
}