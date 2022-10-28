using PipeLight.Steps.Interfaces;

namespace TestProject1.Mocks.Steps;

internal class MockStepAsync : IPipelineStepAsyncHandler<MockPayload, MockPayload>
{
    public async Task<MockPayload> InvokeAsync(MockPayload payload)
    {
        return await Task.FromResult(payload);
    }
}

