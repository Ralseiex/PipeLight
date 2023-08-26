using PipeLight.Abstractions.Steps;

namespace PipeLight.Steps;

internal class FuncPipeStep<T> : IPipelineStep<T>
{
    private readonly Func<T, T> _handler;

    public FuncPipeStep(Func<T, T> handler) => _handler = handler;

    public async Task<T> Execute(T payload) => await Task.FromResult(_handler(payload));
}