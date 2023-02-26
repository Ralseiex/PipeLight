using PipeLight.Abstractions.Steps;

namespace PipeLight.Tests.Mocks.Steps;

internal class MockStep : IPipelineStep<MockPayloadInt>
{
    public async Task<MockPayloadInt> Execute(MockPayloadInt payload)
    {
        return await Task.FromResult(payload);
    }
}