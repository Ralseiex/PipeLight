using PipeLight.Abstractions.Steps;

namespace PipeLight.Tests.Mocks.Steps;


internal class MockStepWithException : IPipelineStep<MockPayloadInt>
{

    public async Task<MockPayloadInt> Execute(MockPayloadInt payload)
    {
        throw new MockException();
    }
}
