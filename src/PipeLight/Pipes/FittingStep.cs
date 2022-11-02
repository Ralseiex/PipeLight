using PipeLight.Pipes.Interfaces;

namespace PipeLight.Pipes;

public class PipeFittingFunc<TIn, TOut> : IPipeFitting<TIn, TOut>
{
    private readonly Func<TIn, TOut> _handler;

    public PipeFittingFunc(Func<TIn, TOut> fittingHandler)
    {
        _handler = fittingHandler;
    }

    public async Task<TOut> FitAsync(TIn payload)
    {
        return await Task.FromResult(_handler(payload));
    }
}
