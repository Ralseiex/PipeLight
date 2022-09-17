namespace PipeLight.Middlewares.Interfaces;

public interface IPipelineMiddleware<TIn, TOut>
{
    Task<TOut> InvokeAsync(TIn data);
}

