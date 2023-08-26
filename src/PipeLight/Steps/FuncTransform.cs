using PipeLight.Abstractions.Steps;

namespace PipeLight.Steps;

public class FuncTransform<TIn, TOut> : IPipelineTransform<TIn, TOut>
{
    private readonly Func<TIn, TOut> _handler;

    public FuncTransform(Func<TIn, TOut> transformHandler) => _handler = transformHandler;

    public async Task<TOut> Transform(TIn payload) => await Task.FromResult(_handler(payload));
}