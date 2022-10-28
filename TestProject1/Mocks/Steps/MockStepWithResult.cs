using PipeLight.Steps.Interfaces;

namespace TestProject1.Mocks.Steps;

internal class MockAsyncStepWithResult : IPipelineStepAsyncHandler<MockPayload, MockPipelineResult>
{
    public async Task<MockPipelineResult> InvokeAsync(MockPayload payload)
    {
        return await Task.FromResult(new MockPipelineResult()
        {
            Value = payload.Value,
            RefValue = payload.RefValue,
        });
    }
}
