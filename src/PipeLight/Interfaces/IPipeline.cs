using PipeLight.Middlewares.Interfaces;

namespace PipeLight.Interfaces;

public interface IPipeline<TIn, TOut>
{
    Task<TOut> PushAsync(TIn data);
    IPipeline<TIn, TNewOut> AddMiddleware<TNewOut>(IPipelineMiddleware<TOut, TNewOut> middleware);
    IPipeline<TIn, TNewOut> AddFunc<TNewOut>(Func<TOut, TNewOut> func);
}