using PipeLight.Abstractions.Steps;

namespace PipeLight.Mocks.Steps;


public class MockStepWithException : IPipelineStep<MockPayloadInt>
{

    public async Task<MockPayloadInt> Execute(MockPayloadInt payload)
    {
        throw new MockException();
    }
}
