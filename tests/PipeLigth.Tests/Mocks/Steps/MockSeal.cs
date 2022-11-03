using PipeLight.Nodes.Steps.Interfaces;

namespace PipeLigth.Tests.Mocks.Steps;

internal class MockSealAsync : ISealedStep<MockPayloadInt>
{
    public async Task ExecuteSealedStepAsync(MockPayloadInt payload)
    {
        await Task.CompletedTask;
    }
}
