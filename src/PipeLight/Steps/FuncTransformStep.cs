using PipeLight.Abstractions.Steps;

namespace PipeLight.Steps;

public class FuncTransformStep<TIn, TOut> : IPipelineTransformStep<TIn, TOut>
{
    private readonly Func<TIn, TOut> _handler;

    public FuncTransformStep(Func<TIn, TOut> transformHandler)
    {
        _handler = transformHandler;
    }

    public async Task<TOut> Transform(TIn payload)
    {
        return await Task.FromResult(_handler(payload));
    }
}
