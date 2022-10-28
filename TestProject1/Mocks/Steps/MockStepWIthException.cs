using PipeLight.Steps.Interfaces;

namespace TestProject1.Mocks.Steps;


internal class MockStepWithExceptionAsync : IPipelineStepAsyncHandler<MockPayload, MockPayload>
{
    public Task<MockPayload> InvokeAsync(MockPayload payload)
    {
        throw new MockException();
    }
}
