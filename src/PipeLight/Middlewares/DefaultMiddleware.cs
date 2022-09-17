using PipeLight.Middlewares.Interfaces;

namespace PipeLight.Middlewares;

public class DefaultMiddleware<TIn> : IPipelineMiddleware<TIn, TIn>
{
    public Task<TIn> InvokeAsync(TIn data)
        => Task.FromResult(data);
}