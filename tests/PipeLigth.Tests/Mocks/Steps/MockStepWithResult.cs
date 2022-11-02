using PipeLight.Pipes.Interfaces;

namespace PipeLigth.Tests.Mocks.Steps;

internal class MockAsyncStepWithResult : IPipeTransform<MockPayloadInt, MockPipelineResult>
{
    public async Task<MockPipelineResult> TransformAsync(MockPayloadInt payload)
    {
        return await Task.FromResult(new MockPipelineResult()
        {
            Value = payload.Value,
            RefValue = payload.RefValue,
        });
    }
}
