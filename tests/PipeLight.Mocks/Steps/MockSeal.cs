using PipeLight.Abstractions.Steps;

namespace PipeLight.Mocks.Steps;

public class MockSeal : IPipelineSealedStep<MockPayloadInt>
{
    public async Task Execute(MockPayloadInt payload)
    {
        await Task.CompletedTask;
    }
}
