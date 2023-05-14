using PipeLight.Abstractions.Steps;

namespace PipeLight.Mocks.Steps;

public class MockWithResult : IPipelineTransform<MockPayloadInt, MockPipelineResult>
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
