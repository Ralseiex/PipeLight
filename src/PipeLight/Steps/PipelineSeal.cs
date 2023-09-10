using PipeLight.Abstractions.Steps;

namespace PipeLight.Steps;

internal sealed class PipelineSeal<TIn> : IPipelineSeal<TIn>
{
    private readonly Action<TIn> _handler;

    public PipelineSeal(Action<TIn> handler) => _handler = handler;

    public async Task Execute(TIn payload) => await Task.Run(() => _handler(payload));
}