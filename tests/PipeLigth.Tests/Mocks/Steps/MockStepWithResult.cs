using PipeLight.Pipes.Interfaces;

namespace PipeLigth.Tests.Mocks.Steps;

internal class MockAsyncStepWithResult : IPipeFitting<MockPayloadInt, MockPipelineResult>
{
    public async Task<MockPipelineResult> FitAsync(MockPayloadInt payload)
    {
        return await Task.FromResult(new MockPipelineResult()
        {
            Value = payload.Value,
            RefValue = payload.RefValue,
        });
    }
}
