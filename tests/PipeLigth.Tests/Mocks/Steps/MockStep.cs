using PipeLight.Nodes.Steps.Interfaces;
namespace PipeLigth.Tests.Mocks.Steps;

internal class MockStepAsync : IPipeStep<MockPayloadInt>
{
    public async Task<MockPayloadInt> ExecuteStepAsync(MockPayloadInt payload)
    {
        return await Task.FromResult(payload);
    }
}

