using PipeLight.Steps.Interfaces;

namespace TestProject1.Mocks.Steps;

internal class MockSealAsync : IPipelineStepAsyncHandler<MockPayload>
{
    public async Task InvokeAsync(MockPayload payload)
    {
        await Task.CompletedTask;
    }
}
