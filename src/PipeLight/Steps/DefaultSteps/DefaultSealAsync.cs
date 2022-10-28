using PipeLight.Steps.Interfaces;

namespace PipeLight.Steps.DefaultSteps;

internal class DefaultSealAsync<TIn> : IPipelineStepAsyncHandler<TIn>
{
    public Task InvokeAsync(TIn payload)
        => Task.CompletedTask;
}