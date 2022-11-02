using PipeLight.Pipes.Interfaces;

namespace PipeLight.Pipes;

public class PipeTransformFunc<TIn, TOut> : IPipeTransform<TIn, TOut>
{
    private readonly Func<TIn, TOut> _handler;

    public PipeTransformFunc(Func<TIn, TOut> transformHandler)
    {
        _handler = transformHandler;
    }

    public async Task<TOut> TransformAsync(TIn payload)
    {
        return await Task.FromResult(_handler(payload));
    }
}
