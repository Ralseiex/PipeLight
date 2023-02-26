using PipeLight.Abstractions.Steps;

namespace PipeLight.Tests.Mocks.Steps;

internal class MockStepWithResult : IPipelineTransformStep<MockPayloadInt, MockPipelineResult>
{
    public async Task<MockPipelineResult> Transform(MockPayloadInt payload)
    {
        return await Task.FromResult(new MockPipelineResult()
        {
            Value = payload.Value,
            RefValue = payload.RefValue,
        });
    }
}
