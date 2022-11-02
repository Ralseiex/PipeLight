using PipeLight.Nodes.Steps.Interfaces;

namespace PipeLigth.Tests.Mocks.Steps;


internal class MockStepWithExceptionAsync : IPipeStep<MockPayloadInt>
{

    public async Task<MockPayloadInt> ExecuteStepAsync(MockPayloadInt payload)
    {
        throw new MockException();
    }
}
