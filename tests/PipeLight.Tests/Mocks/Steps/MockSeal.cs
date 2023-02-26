using PipeLight.Abstractions.Steps;

namespace PipeLight.Tests.Mocks.Steps;

internal class MockSeal : IPipelineSealedStep<MockPayloadInt>
{
    public async Task Execute(MockPayloadInt payload)
    {
        await Task.CompletedTask;
    }
}
