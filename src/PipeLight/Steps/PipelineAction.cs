using PipeLight.Abstractions.Steps;

namespace PipeLight.Steps;

internal sealed class PipelineAction<T> : IPipelineAction<T>
{
    private readonly Func<T, T> _handler;

    public PipelineAction(Func<T, T> handler) => _handler = handler;

    public async Task<T> Execute(T payload) => await Task.FromResult(_handler(payload));
}