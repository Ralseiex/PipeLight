using PipeLight.Abstractions.Steps;

namespace PipeLight.Mocks.Steps;

public class MockStep : IPipelineStep<MockPayloadInt>
{
    public async Task<MockPayloadInt> Execute(MockPayloadInt payload)
    {
        return await Task.FromResult(payload);
    }
}