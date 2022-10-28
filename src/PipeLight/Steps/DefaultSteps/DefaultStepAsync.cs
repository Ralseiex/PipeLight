using PipeLight.Steps.Interfaces;

namespace PipeLight.Steps.DefaultSteps;

internal class DefaultStepAsync<TIn> : IPipelineStepAsyncHandler<TIn, TIn>
{
    public Task<TIn> InvokeAsync(TIn payload)
        => Task.FromResult(payload);
}
