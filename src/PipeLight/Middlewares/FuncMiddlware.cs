using PipeLight.Middlewares.Interfaces;

namespace PipeLight.Middlewares;

public class FuncMiddlware<TIn, TOut> : IPipelineMiddleware<TIn, TOut>
{
    private readonly Func<TIn, TOut> _handler;

    public FuncMiddlware(Func<TIn, TOut> handler)
    {
        _handler = handler;
    }
    public Task<TOut> InvokeAsync(TIn data)
        => Task.FromResult(_handler(data));
}
